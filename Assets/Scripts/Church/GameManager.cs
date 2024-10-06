using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

namespace Church
{
    public class GameManager : MonoBehaviour
    {
        public GameObject backWalls;
        public GameObject stairs;
        public Volume volume;
        public float decSpeed;
        
        private void Awake()
        {
            Molly.RiseEvent += OnRiseEvent;
            FallTrigger.FallEvent += OnFallEvent;
        }

        private void OnRiseEvent()
        {
            backWalls.SetActive(false);
            stairs.SetActive(true);
        }

        private void OnFallEvent()
        {
            var stairsGameObjects = GameObject.FindGameObjectsWithTag("Stair");
            foreach (var stairsGameObject in stairsGameObjects)
            {
                stairsGameObject.SetActive(false);
            }

            volume.profile.TryGet(out LensDistortion lensDistortion);
            DOTween.To(() => lensDistortion.intensity.value, value => lensDistortion.intensity.value = value, 1, 3);

            volume.profile.TryGet(out ColorAdjustments colorAdjustments);
            DOTween.To(() => colorAdjustments.contrast.value, value => colorAdjustments.contrast.value = value, -100, 9)
                .SetEase(Ease.InCubic).OnComplete(()=>SceneManager.LoadScene("Party"));
        }
    }
}
