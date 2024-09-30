using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        entered = false;
        volume.profile.TryGet(out vignette);
        vignetteCloseTime = 2.0f;
        vignetteCurve = AnimationCurve.EaseInOut(0, 0, 2.0f, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if (entered && Input.GetKey("e"))
        {
           

            // Optional: Reset or pause timer once curve reaches the end
            // if (timer >= vignetteCloseTime)
            // {
            //     timer = vignetteCloseTime; // Stops the timer once it reaches the end of the curve
            // }
            // vignetteCloseTime = Time.realtimeSinceStartup;
            // float vignetteIntensity = vignetteCurve.Evaluate(Time.realtimeSinceStartup - vignetteCloseTime);
            // vignette.intensity.value = vignetteIntensity;

            StartCoroutine("EnterMind");
            Debug.Log("HERE");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
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
        }
    }

    IEnumerator EnterMind()
    {
         timer += Time.deltaTime;

        // Calculate the normalized time (between 0 and 1)
        float normalizedTime = Mathf.Clamp01(timer / vignetteCloseTime); // Prevents overshooting beyond 1

        // Evaluate the vignette intensity based on the animation curve
        float vignetteIntensity = vignetteCurve.Evaluate(normalizedTime);
        Debug.Log("vignetteIntensity: " + vignetteIntensity);
        vignette.intensity.value = vignetteIntensity;
        if (vignetteIntensity > 1)
        {
            SceneManager.LoadScene(scenename);
        }
        yield return new WaitForSeconds(2.0f);
    }
}
