using System;
using UnityEngine;

namespace Player
{
    public class TempDamage : MonoBehaviour
    {
        private bool _hasCollided = false;
        public LayerMask layerMask;
        
        void Start()
        {
            
        }

    
        void Update()
        {
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_hasCollided && layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                HealthBar.Health -= 10f;
                _hasCollided = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _hasCollided = false;
        }
    }
}
