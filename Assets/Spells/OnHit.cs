using System;
using UnityEngine;

namespace Spells
{
    public class OnHit : MonoBehaviour
    {
        
        private bool _hasCollided = false;
        public LayerMask layerMask;
        public GameObject win;
        private void OnTriggerEnter(Collider other)
        {
            if (!_hasCollided && layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                _hasCollided = true;
                // Destroy this and the other object
                Instantiate(win, transform.position, Quaternion.identity);
                Destroy(gameObject);
                Destroy(other.gameObject);
            }
            _hasCollided = false;
        }
    }
}
