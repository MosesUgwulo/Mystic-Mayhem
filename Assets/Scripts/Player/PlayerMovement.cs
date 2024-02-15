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
        private bool _isChargingMana; // Is the player charging mana?
        
        private bool _isFirstFrame = true; // Is this the first frame of the game?
        
        private void Start()
        {
            ResetPlayer(); // Reset the player to the spawnpoint at the start of the game
        }

        public void ResetPlayer()
        {
            var spawnpoint = GameObject.FindGameObjectWithTag("PlayerSpawnpoint"); // Find the spawnpoint
            if (spawnpoint == null)
            {
                Debug.LogError("No spawnpoint found!");
            }
            else
            {
                controller.Move(spawnpoint.transform.position - transform.position); // Move the player to the spawnpoint
            }
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
            Vector3 movement = transformVar.right * horizontalInput + transformVar.forward * verticalInput; // Calculate movement vector based on input
        
            controller.Move(movement * (speed * Time.deltaTime)); // Move the player based on the movement vector
            

            if (Input.GetButtonDown("Jump") && _isGrounded) // If player presses jump and is grounded
            {
                _velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity); // Set velocity to jump height
            }
        
            _velocity.y += gravity * Time.deltaTime; // Apply gravity
        
            controller.Move(_velocity * Time.deltaTime); // Apply gravity
            
            // Pressing R returns the player to the start
            if (Input.GetKey(KeyCode.R))
            {
                ResetPlayer();
            }

            if (_isChargingMana)
            {
                ManaBar.Mana += 0.25f;
            }
            
            if (_isFirstFrame) // This only runs on the first frame and then never again
            {
                _isFirstFrame = false;
                ResetPlayer();
            }
            
        }

        public void OnChargeMana(InputAction.CallbackContext ctx) // This function is called when the player presses the charge mana button
        {
            _isChargingMana = ctx.control.IsPressed(); // Set _isChargingMana to true if the button is pressed
        }
    }
}
