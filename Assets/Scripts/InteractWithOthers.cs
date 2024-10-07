using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class InteractWithOthers : MonoBehaviour
{

    public GameObject text;
    public string scenename;
    public bool entered;
    public Volume volume;
    [SerializeField] private AnimationCurve vignetteCurve;

    private float vignetteCloseTime;
    private Vignette vignette;
    private float timer = 0f;

    private Vector3 playerStartPosition;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        entered = false;
        volume.profile.TryGet(out vignette);
        vignetteCloseTime = 2.0f;
        vignetteCurve = AnimationCurve.EaseInOut(0, 0, 2.0f, 100);
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (entered && Input.GetKey("e"))
        {
            timer += Time.deltaTime;

            // Calculate the normalized time (between 0 and 1)
            float normalizedTime = Mathf.Clamp01(timer / vignetteCloseTime); // Prevents overshooting beyond 1

            // Evaluate the vignette intensity based on the animation curve
            float vignetteIntensity = vignetteCurve.Evaluate(normalizedTime);
            Debug.Log("vignetteIntensity: " + vignetteIntensity);
            vignette.intensity.value = vignetteIntensity;

            playerStartPosition = player.transform.position;

            // Store the player's position in PlayerPrefs
            PlayerPrefs.SetFloat("PlayerPosX", playerStartPosition.x);
            PlayerPrefs.SetFloat("PlayerPosY", playerStartPosition.y);
            PlayerPrefs.SetFloat("PlayerPosZ", playerStartPosition.z);

            if (vignetteIntensity > 2)
            {
                SceneManager.LoadScene(scenename);
            }
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            string name = transform.parent.name;
            text.GetComponent<TextMeshProUGUI>().text = $"Hold E to Enter {name}'s Mind"; // Update the text with the parent name
            text.SetActive(true);
            entered = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            text.SetActive(false);
            entered = false;
            vignette.intensity.value = 0;
        }
    }
}
