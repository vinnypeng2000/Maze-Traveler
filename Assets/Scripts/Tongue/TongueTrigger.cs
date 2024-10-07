using System;
using Ocean;
using UnityEngine;

namespace Tongue
{
    public class TongueTrigger : MonoBehaviour
    {
        public static event Action TongueEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TongueEvent?.Invoke();
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
