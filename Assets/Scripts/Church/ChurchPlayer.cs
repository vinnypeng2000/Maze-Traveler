using System;
using DG.Tweening;
using UnityEngine;

namespace Church
{
    public class ChurchPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform mainCamera;
        [SerializeField] private Transform mollyPos;
        [SerializeField] private Rigidbody playerRb;

        private bool _isFalling;
        
        private void Awake()
        {
            FallTrigger.FallEvent += OnFallEvent;
        }

        private void OnFallEvent()
        {
            playerController.enabled = false;
            characterController.enabled = false;
            _isFalling = true;
            playerRb.velocity = new Vector3(0, -2f, 0);
        }

        private void Update()
        {
            if (_isFalling)
            {
                mainCamera.LookAt(mollyPos);
            }
        }
    }
}
