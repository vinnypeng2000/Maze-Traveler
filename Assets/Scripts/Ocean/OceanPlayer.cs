using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Ocean
{
    public class OceanPlayer : MonoBehaviour
    {
        [SerializeField] private Animator playerAnim;

        private bool _isBacking;
        private bool _isLefting;

        private void Update()
        {
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
        }
    }
}