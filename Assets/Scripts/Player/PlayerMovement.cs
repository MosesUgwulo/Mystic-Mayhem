using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
    
        public CharacterController controller; // Character controller
        public Transform groundCheck; // Ground check transform
        public LayerMask groundMask; // Ground mask
    
        public float speed = 10f; // Movement speed
        public float gravity = -9.81f; // Gravity
        public float groundDistance = 0.4f; // Distance to ground
        public float jumpHeight = 3f; // Jump height
    
        private Vector3 _velocity; // Velocity vector
        private bool _isGrounded; // Is the player grounded?
        private bool _isChargingMana;

        private void Start()
        {
            
        }
        
        private void Update()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if player is grounded
        
            if (_isGrounded && _velocity.y < 0) // If player is grounded and falling
            {
                _velocity.y = -2f; // Set velocity to -2
            }
        
            float horizontalInput = Input.GetAxisRaw("Horizontal"); // Horizontal input
            float verticalInput = Input.GetAxisRaw("Vertical"); // Vertical input

            var transformVar = transform;
            Vector3 movement = transformVar.right * horizontalInput + transformVar.forward * verticalInput; // Calculate movement vector
        
            controller.Move(movement * (speed * Time.deltaTime)); // Move the player

            if (Input.GetButtonDown("Jump") && _isGrounded) // If player presses jump and is grounded
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // Set velocity to jump height
            }
        
            _velocity.y += gravity * Time.deltaTime; // Apply gravity
        
            controller.Move(_velocity * Time.deltaTime); // Apply gravity
        
            // Pressing R returns the player to the start
            if (Input.GetKey(KeyCode.R))
            {
                transform.position = new Vector3(0, 9, 0);
            }

            if (_isChargingMana)
            {
                ManaBar.Mana += 0.5f;
            }
        }

        public void OnChargeMana(InputAction.CallbackContext ctx)
        {
            _isChargingMana = ctx.control.IsPressed();
        }
    }
}
