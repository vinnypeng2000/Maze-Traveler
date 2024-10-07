using UnityEngine;

namespace Church
{
    public class Stairs : MonoBehaviour
    {
        public MeshRenderer mesh1;
        public MeshRenderer mesh2;

        private void OnCollisionEnter(Collision other)
        {
            mesh1.enabled = true;
            mesh2.enabled = true;
        }
    }
}
