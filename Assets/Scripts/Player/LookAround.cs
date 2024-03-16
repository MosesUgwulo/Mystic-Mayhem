using UnityEngine;

namespace Player
{
    public class LookAround : MonoBehaviour
    {
        public Transform orientation;
        public Transform player;
        public float mouseSensitivityX;
        public float mouseSensitivityY;
        private float _xRotation;
        private float _yRotation;
        
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        private void Update()
        {
            // Get mouse input
            var mouseX = Input.GetAxis("Mouse X") * (mouseSensitivityX * Time.deltaTime);
            var mouseY = Input.GetAxis("Mouse Y") * (mouseSensitivityY * Time.deltaTime);
            
            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
            
            _yRotation += mouseX;
            
            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
            orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
            player.rotation = Quaternion.Euler(0, _yRotation, 0);
        }
    }
}
