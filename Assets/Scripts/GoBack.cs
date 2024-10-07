using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string targetScene; // The name of the scene to switch to

    private void OnCollisionEnter(Collision other)
    {
        // Check if the object that collided with this one is the player
        if (other.gameObject.CompareTag("Player"))
        {
            
            // Load the target scene asynchronously
            SceneManager.LoadScene(targetScene);
            
        }
    }

}