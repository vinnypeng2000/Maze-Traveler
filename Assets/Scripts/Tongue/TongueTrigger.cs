using System;
using DG.Tweening;
using Ocean;
using UnityEngine;

namespace Tongue
{
    public class TongueTrigger : MonoBehaviour
    {

        public Transform sarahTongue;
        public Transform chadTongue;

        public static event Action TongueEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TongueEvent?.Invoke();
                transform.parent.gameObject.SetActive(false);
                sarahTongue.DOScale(new Vector3(5, 5, 65f), 10f);
                chadTongue.DOScale(new Vector3(5, 5, 65f), 10f);
            }
        }
    }
}
