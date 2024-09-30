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
    public AnimationCurve vignetteCurve;

    private float vignetteCloseTime;
    private Vignette vignette;

    // Start is called before the first frame update
    void Start()
    {
        text.SetActive(false);
        entered = false;
        volume.profile.TryGet(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        if (entered && Input.GetKeyDown("e"))
        {
            vignetteCloseTime = Time.realtimeSinceStartup;
            float vignetteIntensity = vignetteCurve.Evaluate(Time.realtimeSinceStartup - vignetteCloseTime);
            vignette.intensity.value = vignetteIntensity;
            StartCoroutine("EnterMind");
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
        
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(scenename);
    }
}
