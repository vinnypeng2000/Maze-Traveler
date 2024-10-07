using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shadow : MonoBehaviour
{
    public Vector3 targetScale; // The target scale to reach
    public float duration; // Duration of the scaling effect
    public float delay; // Delay before starting the scaling
    public Image overlayImage;

    private void Start()
    {
        // Start the scaling effect with a delay
        StartCoroutine(ScaleCoroutine());
    }

    private IEnumerator ScaleCoroutine()
    {
        yield return new WaitForSeconds(delay); // Wait for the specified delay

        Vector3 initialScale = transform.localScale; // Store the initial scale
        float timeElapsed = 0f; // Initialize the elapsed time

        while (timeElapsed < duration)
        {
            // Interpolate the scale over time
            float t = timeElapsed / duration; // Calculate the normalized time (0 to 1)
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t); // Set the new scale

            // Darken the screen by adjusting the overlay image alpha
            overlayImage.color = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 0.7f, t)); // Adjust the alpha to darken

            timeElapsed += Time.deltaTime; // Increment the elapsed time
            yield return null; // Wait for the next frame
        }

        // Ensure the object ends at the target scale
        transform.localScale = targetScale;
        overlayImage.color = new Color(0f, 0f, 0f, 0.7f); // Set the overlay to its final darkened state
    }
}
