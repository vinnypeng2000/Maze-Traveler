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
        }
    }
}