using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterSceneBlack : MonoBehaviour
{
    public string sceneText;
    
    [SerializeField] private Image blackFade;
    [SerializeField] private TextMeshProUGUI enterSceneText;

    private void Awake()
    {
        enterSceneText.text = sceneText;
    }

    private void Start()
    {
        blackFade.DOFade(0, 2.5f);
        enterSceneText.DOFade(0, 5).OnComplete(() => gameObject.SetActive(false));
    }
    
}
