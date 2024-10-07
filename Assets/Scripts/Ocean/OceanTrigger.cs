using System;
using UnityEngine;

namespace Ocean
{
    public class OceanTrigger : MonoBehaviour
    {
        public static event Action OceanEvent;
        
        private bool _isNearPlayer;
        
        private void Update()
        {
            if (_isNearPlayer && Input.GetKeyDown(KeyCode.E))
            {
                OceanEvent?.Invoke();
                gameObject.SetActive(false);
            }
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
}
