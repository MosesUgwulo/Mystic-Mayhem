using UnityEngine;

namespace Player
{
    public class MoveCam : MonoBehaviour
    {
        public Transform camPos;
        void Update()
        {
            transform.position = camPos.position;
        }
    }
}
