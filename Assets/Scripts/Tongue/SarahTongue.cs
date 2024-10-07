using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahTongue : MonoBehaviour
{
    public GameObject molly;
    
    private bool _isNearPlayer;
    private bool _isTriggered;
    
    private void Update()
    {
        if (_isNearPlayer && !_isTriggered && Input.GetKeyDown(KeyCode.E))
        {
            _isTriggered = true;
            StartCoroutine(FinishInteractive());
        }
    }

    private IEnumerator FinishInteractive()
    {
        yield return new WaitForSeconds(3f);
        molly.SetActive(true);
        transform.parent.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isNearPlayer = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isNearPlayer = false;
        }
    }
}
