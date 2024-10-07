using System;
using UnityEngine;

namespace Tongue
{
    public class TonguePlayer : MonoBehaviour
    {
        [SerializeField] private GameObject tongue;
        [SerializeField] private Animator playerAnim;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CharacterController characterController;
        
        private bool _isBacking;
        private bool _isLefting;
        private bool _isTongueStatus;

        private void Awake()
        {
            TongueTrigger.TongueEvent += OnTongueEvent;
        }

        private void Update()
        {
            MoveAnim();
        }

        private void OnTongueEvent()
        {
            _isTongueStatus = true;
            playerController.moveSpeed = 25f;
            //characterController.enabled = false;
            playerAnim.SetBool("IsWalking", false);
            playerAnim.gameObject.SetActive(false);
            tongue.SetActive(true);
        }

        private void MoveAnim()
        {
            if(_isTongueStatus) return;
            
            if (Input.GetAxisRaw("Horizontal").Equals(0) && Input.GetAxisRaw("Vertical").Equals(0))
            {
                playerAnim.SetBool("IsWalking", false);
            }
            else
            {
                playerAnim.SetBool("IsWalking", true);
            }
        }
    }
}