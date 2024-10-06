using System;
using DG.Tweening;
using UnityEngine;

namespace Church
{
    public class Molly : MonoBehaviour
    {
        private bool _isNearPlayer;
        private bool _isRising;
        
        public static event Action RiseEvent;

        private void Awake()
        {
            RiseEvent += OnRiseEvent;
            FallTrigger.FallEvent += OnFallEvent;
        }

        private void Update()
        {
            if (_isNearPlayer && !_isRising && Input.GetKeyDown(KeyCode.E))
            {
                RiseEvent?.Invoke();
            }
        }

        private void OnRiseEvent()
        {
            _isRising = true;
            transform.parent = GameObject.FindGameObjectWithTag("Player").transform;
            transform.DOLocalMove(new Vector3(0.27363804f,1.48200035f,3.35067821f), 1f);
        }

        private void OnFallEvent()
        {
            transform.SetParent(null);
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
