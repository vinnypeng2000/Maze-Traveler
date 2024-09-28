Shader "Hidden/VolumetricFog2/DistantFog"
{
	Properties
	{
		[HideInInspector] _MainTex("Noise Texture", 2D) = "white" {}
		[HideInInspector] _Color("Color", Color) = (1,1,1)
		[HideInInspector] _DistantFogData("Distant Fog Data", Vector) = (100,0.1,400,0.5)
		[HideInInspector] _DistantFogData2("Base Altitude", Vector) = (0, 1, 0, 0)
		[HideInInspector] _LightColor("Light Color", Color) = (1,1,1)
		[HideInInspector] _LightDiffusionPower("Sun Diffusion Power", Range(1, 64)) = 32
		[HideInInspector] _LightDiffusionIntensity("Sun Diffusion Intensity", Range(0, 1)) = 0.4
		[HideInInspector] _SunDir("Sun Direction", Vector) = (1,0,0)
	}
		SubShader
		{
			Tags { "RenderType" = "Transparent" "Queue" = "Transparent-1" "DisableBatching" = "True" "IgnoreProjector" = "True" "RenderPipeline" = "UniversalPipeline" }
			Blend SrcAlpha OneMinusSrcAlpha
			ZTest Always
			Cull Off
			ZWrite Off

			Pass
			{
				Name "Distant Fog"
				Tags { "LightMode" = "UniversalForward" }
				HLSLPROGRAM
				#pragma prefer_hlslcc gles
				#pragma exclude_renderers d3d11_9x
				#pragma target 3.0
				#pragma multi_compile _ VF2_DEPTH_PREPASS
				#pragma vertex vert
				#pragma fragment frag

				#include "CommonsURP.hlsl"
				#include "Primitives.cginc"
				#include "Raymarch2D.cginc"

				float4 _DistantFogData;
				#define START_DISTANCE _DistantFogData.x
				#define DISTANCE_DENSITY _DistantFogData.y
				#define MAX_HEIGHT _DistantFogData.z
				#define HEIGHT_DENSITY _DistantFogData.w

				float4 _DistantFogData2;
				#define BASE_ALTITUDE _DistantFogData2.x
				#define MIN_ALTITUDE _DistantFogData2.y

				struct appdata
				{
					float4 vertex : POSITION;
					float2 uv  : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f
				{
					float4 pos    : SV_POSITION;
					float4 scrPos : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				v2f vert(appdata v)
				{
					v2f o;

					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

					o.pos = TransformObjectToHClip(v.vertex.xyz);
					o.scrPos = ComputeScreenPos(o.pos);

					return o;
				}



				half4 frag(v2f i) : SV_Target {
					UNITY_SETUP_INSTANCE_ID(i);
					UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

					float2 uv = i.scrPos.xy / i.scrPos.w;

					#if UNITY_REVERSED_Z
						float depth = GetRawDepth(uv);
					#else
						float depth = GetRawDepth(uv);
						depth = depth * 2.0 - 1.0;
					#endif
					float3 wpos = ComputeWorldSpacePosition(uv, depth, unity_MatrixInvVP);

					float3 rayStart = GetRayStart(wpos);
					float3 ray = wpos - rayStart;
					
                   	float t1 = length(ray);
					float3 rayDir = ray / t1;
					
					float3 hitPos = t1 * rayDir;

					float maxZ = _ProjectionParams.z * 0.99;
					float startDistance = min(maxZ, START_DISTANCE);
					float d = (t1 - startDistance) * DISTANCE_DENSITY;
					float hitPosY = max(MIN_ALTITUDE, hitPos.y + rayStart.y - BASE_ALTITUDE);
					float h = (hitPosY != 0 ? MAX_HEIGHT / abs(hitPosY) : MAX_HEIGHT) * HEIGHT_DENSITY;
					float f = min(d, h);
					f = max(f, 0);

					half sum = exp2(-f);
					sum = 1.0 - saturate(sum);

					half4 color = half4(_Color.rgb, sum * _Color.a);
					if (t1 > maxZ) { // skybox
						half diffusionIntensity = GetDiffusionIntensity(rayDir);
						color.rgb += diffusionIntensity;
					}
					color.rgb *= _LightColor.rgb;
					return color;
				}
				ENDHLSL
			}

		}
}
