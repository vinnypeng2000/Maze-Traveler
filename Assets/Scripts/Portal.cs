using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string sceneName;
    
    private bool isNearPlayer;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }

    private void Update()
    {
        if (isNearPlayer && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
