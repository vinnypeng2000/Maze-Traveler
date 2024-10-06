using System;
using UnityEngine;

namespace Church
{
    public class FallTrigger : MonoBehaviour
    {
        public static event Action FallEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                FallEvent?.Invoke();
            }
        }
    }
}
