using System;
using Player;
using UnityEngine;

namespace Spells
{
    public class OnHit : MonoBehaviour
    {
        
        private bool _hasCollided = false;
        public LayerMask layerMask;
        private void OnTriggerEnter(Collider other)
        {
            if (!_hasCollided && layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                _hasCollided = true;
                // Do damage calculations here. As well as any other effects.
                Destroy(gameObject);
            }
            _hasCollided = false;
        }
    }
}
