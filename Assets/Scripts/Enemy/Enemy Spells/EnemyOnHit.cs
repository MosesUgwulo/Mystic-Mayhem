using Player;
using Spells;
using UnityEngine;

namespace Enemy.Enemy_Spells
{
    public class EnemyOnHit : MonoBehaviour
    {
        private bool _hasCollided = false;
        public LayerMask layerMask;
        private MagicSystem _magicSystem;
        void Start()
        {
            _magicSystem = GetComponent<MagicSystem>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_hasCollided && layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                _hasCollided = true;
                if (other.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Player has been hit for " + _magicSystem.damage + " damage");
                    HealthBar.Health -= _magicSystem.damage;
                    
                }
            }
        }
        
    }
}
