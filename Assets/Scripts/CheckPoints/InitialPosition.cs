using UnityEngine;

namespace CheckPoints
{
    public class InitialPosition : MonoBehaviour
    {
        public Vector3 InitialPos { get; private set; }

        private void Awake()
        {
            // Record the initial position in Awake
            InitialPos = transform.position;
        }
    }
}