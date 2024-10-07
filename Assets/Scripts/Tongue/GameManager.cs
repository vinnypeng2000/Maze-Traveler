using System;
using DG.Tweening;
using UnityEngine;

namespace Tongue
{
    public class GameManager : MonoBehaviour
    {
        public CharacterController characterController;
        public PlayerController playerController;
        public Transform playerCamera;
        public Transform cameraFrontPos;
        public Transform cameNormalPos;
        public GameObject sarah;
        public GameObject chad;
        
        private void Awake()
        {
            TongueTrigger.TongueEvent += OnTongueEvent;
        }

        private void OnTongueEvent()
        {
            chad.SetActive(true);
            characterController.enabled = false;
            playerController.enabled = false;
            sarah.SetActive(true);
            var cameraSeq = DOTween.Sequence();
            cameraSeq.Append(playerCamera.DOLocalMove(cameraFrontPos.localPosition, 1.5f))
                .Insert(0, playerCamera.DOLocalRotate(cameraFrontPos.localRotation.eulerAngles, 1.5f))
                .Append(playerCamera.DOLocalMove(cameNormalPos.localPosition, 1.5f).SetDelay(5f))
                .Insert(0, playerCamera.DOLocalRotate(cameNormalPos.localRotation.eulerAngles, 1.5f)
                    .SetDelay(5f).OnComplete(FinishRotateCamera));
        }
        
        private void FinishRotateCamera()
        {
            characterController.enabled = true;
            playerController.enabled = true;
        }
    }
}
