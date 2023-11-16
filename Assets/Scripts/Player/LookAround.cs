using UnityEngine;

namespace Player
{
    public class LookAround : MonoBehaviour
    {
    
        public float mouseSensitivity = 100f; // Mouse sensitivity
        public Transform playerTransform; // Player's transform
    
        float _rotationX = 0f; // Rotation around X axis
    
        // Start is called before the first frame update
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen
        }

        // Update is called once per frame
        void Update()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; // Horizontal mouse movement
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; // Vertical mouse movement
        
            // Rotate camera vertically
            _rotationX -= mouseY; 
            _rotationX = Mathf.Clamp(_rotationX, -90f, 90f); // Clamp rotation to prevent camera from flipping
            transform.localRotation = Quaternion.Euler(_rotationX, 0f, 0f); 
        
            playerTransform.Rotate(Vector3.up * mouseX); // Rotate player horizontally
        }
    }
}
