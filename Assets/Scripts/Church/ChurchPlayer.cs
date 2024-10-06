using System;
using DG.Tweening;
using Unity.VisualScripting;
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
        [SerializeField] private Animator playerAnim;

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

            MoveAnim();
        }

        private void MoveAnim()
        {
            if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0))
            {
                playerAnim.SetBool("IsWalking", false);
            }
            else
            {
                playerAnim.SetBool("IsWalking", true);
            }

            if (Input.GetAxisRaw("Vertical").Equals(1))
            {
                playerAnim.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
            if (Input.GetAxisRaw("Vertical").Equals(-1))
            {
                playerAnim.transform.rotation = Quaternion.Euler(Vector3.zero);
            }
        }
    }
}
