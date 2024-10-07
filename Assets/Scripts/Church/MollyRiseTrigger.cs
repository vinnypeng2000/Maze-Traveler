using UnityEngine;

namespace Church
{
    public class MollyRiseTrigger : MonoBehaviour
    {
        private bool _isTriggered;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !_isTriggered)
            {
                _isTriggered = true;
                FindObjectOfType<Molly>().OnMollyRiseTrigger();
            }
        }
    }
}
