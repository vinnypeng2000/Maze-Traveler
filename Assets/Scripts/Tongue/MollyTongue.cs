using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tongue
{
    public class MollyTongue : MonoBehaviour
    {
        public Image exitBlackFade;
        
        private bool _isNearPlayer;
        private bool _isTriggered;
    
        private void Update()
        {
            if (_isNearPlayer && !_isTriggered && Input.GetKeyDown(KeyCode.E))
            {
                _isTriggered = true;
                exitBlackFade.gameObject.SetActive(true);
                exitBlackFade.DOFade(1, 5f).OnComplete(()=> SceneManager.LoadScene("Party"));
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
