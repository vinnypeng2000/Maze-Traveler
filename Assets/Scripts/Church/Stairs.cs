using UnityEngine;

namespace Church
{
    public class Stairs : MonoBehaviour
    {
        public GameObject stairs;
        
        private readonly Vector3 _offset = new(0, 1.637f - 0.97f, -11.458f + 10.549f);    
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Instantiate(stairs, transform.parent.position + _offset, Quaternion.identity);
            }
        }
    }
}
