using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ocean
{
    public class OceanManager : MonoBehaviour
    {
        public Transform catcher;
        public GameObject layingSarah;
        public GameObject playerIsland;
        public Image blackFade;
        
        private void Awake()
        {
            OceanTrigger.OceanEvent += OnOceanEvent;
        }

        private void OnOceanEvent()
        {
            catcher.gameObject.SetActive(true);
            var catcherSeq = DOTween.Sequence();
            catcherSeq.Append(catcher.DOMoveY(0, 5).OnComplete(()=> layingSarah.SetActive(true)))
                .Append(catcher.DOMoveY(35, 2).SetDelay(2).OnComplete(FinishCatch));
        }

        private void FinishCatch()
        {
            StartCoroutine(VanishIsland());
        }

        private IEnumerator VanishIsland()
        {
            yield return new WaitForSeconds(5);
            playerIsland.SetActive(false);
            blackFade.gameObject.SetActive(true);
            blackFade.DOFade(1, 10).OnComplete(()=>SceneManager.LoadScene("Party"));
        }
    }
}
