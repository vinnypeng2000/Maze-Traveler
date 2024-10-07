using System;
using UnityEngine;

namespace Tongue
{
    public class GameManager : MonoBehaviour
    {
        public GameObject sarah;

        private void Awake()
        {
            TongueTrigger.TongueEvent += OnTongueEvent;
        }

        private void OnTongueEvent()
        {
            sarah.SetActive(true);
        }
    }
}
