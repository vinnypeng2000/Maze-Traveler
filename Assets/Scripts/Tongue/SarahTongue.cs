using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Tongue
{
    public class SarahTongue : MonoBehaviour
    {
        public GameObject molly;
        public GameObject sarahWall;
        public Transform mollyTongue;
    
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
            sarahWall.SetActive(false);
            mollyTongue.DOScale(new Vector3(5, 5, 120), 3);
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
