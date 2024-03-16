using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public LayerMask groundMask;
        public Transform orientation;
        public float moveSpeed;
        public float decelerationRate;
        public float jumpForce;
        public float jumpCooldown;
        public float airMultiplier;
        public float playerHeight;
        public float drag;
        public float maxSlopeAngle;
        private Rigidbody _rb;
        private GameObject _interactableNpc;
        private Vector3 _moveDirection;
        private RaycastHit _slopeHit;
        private float _horizontalInput;
        private float _verticalInput;
        private bool _isGrounded;
        private bool _readyToJump;
        private bool _exitingSlope;
        
        private bool _isChargingMana; // Is the player charging mana?
        

        

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.freezeRotation = true;
            _readyToJump = true;
            InvokeRepeating(nameof(ResetPlayer), 0, 3);
        }

        private void GetInput()
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            _verticalInput = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.Space) && _readyToJump && _isGrounded)
            {
                _readyToJump = false; // Set _readyToJump to false
                Jump(); // Call the Jump function
                Invoke(nameof(ResetJump), jumpCooldown); // This allows the player to keep jumping while the button is held down
            }
    }

        private void Move()
        {
            _moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput; // Calculate the move direction
            
            // Add gravity to the player
            if (!_isGrounded) _rb.AddForce(Vector3.down * 10f, ForceMode.Acceleration);
            
            if (OnSlope() && !_exitingSlope)
            {
                _rb.AddForce(GetSlopeDirection() * (moveSpeed * 20f), ForceMode.Force);

                if (_rb.velocity.y > 0)
                {
                    _rb.AddForce(Vector3.down * 80f, ForceMode.Force);
                }
            }

            if (_horizontalInput != 0 || _verticalInput != 0)
            {
                if (_isGrounded) _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f), ForceMode.Force); // Add force to the player
                else if (!_isGrounded) _rb.AddForce(_moveDirection.normalized * (moveSpeed * 10f * airMultiplier), ForceMode.Force); // Add force to the player with air multiplier
            }
            else
            {
                Vector3 horizontalVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                Vector3 decelerationVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, Time.deltaTime * decelerationRate);
                _rb.velocity = new Vector3(decelerationVelocity.x, _rb.velocity.y, decelerationVelocity.z);
            }

            _rb.useGravity = !OnSlope();
        }

        private void Jump()
        {
            _exitingSlope = true;
            
            _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z); // Set the y component of the velocity to 0
            _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); // Add force to the player in the up direction
        }
        
        private void ResetJump()
        {
            _readyToJump = true; // Set _readyToJump to true
            _exitingSlope = false;
        }

        private void LimitSpeed()
        {
            if (OnSlope() && !_exitingSlope)
            {
                if (_rb.velocity.magnitude > moveSpeed) _rb.velocity = _rb.velocity.normalized * moveSpeed;
            }
            else
            {
                Vector3 flatVelocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z); // Get the velocity without the y component
            
                if (flatVelocity.magnitude > moveSpeed) // If the velocity is greater than the move speed
                {
                    Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed; // Normalize the velocity and multiply it by the move speed
                    _rb.velocity = new Vector3(limitedVelocity.x, _rb.velocity.y, limitedVelocity.z); // Set the y component of the limited velocity to the y component of the original velocity
                }
            }
        }

        private bool OnSlope()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, playerHeight * 0.5f + 0.3f))
            {
                float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
                return angle < maxSlopeAngle && angle != 0;
            }

            return false;
        }

        private Vector3 GetSlopeDirection()
        {
            return Vector3.ProjectOnPlane(_moveDirection, _slopeHit.normal).normalized;
        }

        private void ResetPlayer()
        {
            var spawnpoint = GameObject.Find("SpawnPoint"); // Find the spawnpoint
            transform.position = spawnpoint.transform.position;
            CancelInvoke(nameof(ResetPlayer));
        }

        
        private void Update()
        {
            _isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundMask); // Check if the player is grounded
            GetInput();
            LimitSpeed();
            
            if (_isGrounded) _rb.drag = drag; // If the player is grounded, apply drag
            else 
                _rb.drag = 0; // If the player is not grounded, don't apply drag

            if (_isChargingMana)
            {
                ManaBar.Mana += 0.25f;
            }
            
            if (Input.GetKeyDown(KeyCode.I))
            {
                Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
                Cursor.visible = !Cursor.visible;
            }
            
            if (Input.GetKeyDown(KeyCode.E) && _interactableNpc != null)
            {
                _interactableNpc.GetComponent<HUD.Interactable>().Interact();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Interact"))
            {
                _interactableNpc = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("Interact"))
            {
                _interactableNpc = null;
            }
        }

        private void FixedUpdate()
        {
            Move();
        }

        public void OnChargeMana(InputAction.CallbackContext ctx) // This function is called when the player presses the charge mana button
        {
            _isChargingMana = ctx.control.IsPressed(); // Set _isChargingMana to true if the button is pressed
        }
    }
}
