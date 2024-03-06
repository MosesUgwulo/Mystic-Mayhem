using System.Collections.Generic;
using Spells;
using UnityEngine;

namespace Player.Player_Spells
{
    public class PlayerOnHit : MonoBehaviour
    {
        
        private bool _hasCollided = false;
        public LayerMask layerMask;
        public float trackingSpeed = 10.0f;
        public float trackingRange = 10.0f;
        public GameObject target;
        public Enemy.Enemy enemy;
        public bool isFirstTarget = false;
        public SphereCollider sphereCollider;
        private MagicSystem _magicSystem;

        private Dictionary<(MagicSystem.DamageType, Enemy.Enemy.ElementTypes), float> _damageMultipliers =
            new Dictionary<(MagicSystem.DamageType, Enemy.Enemy.ElementTypes), float>()
            {
                {(MagicSystem.DamageType.Fire, Enemy.Enemy.ElementTypes.Fire), 1.0f},
                {(MagicSystem.DamageType.Fire, Enemy.Enemy.ElementTypes.Water), 0.5f},
                {(MagicSystem.DamageType.Fire, Enemy.Enemy.ElementTypes.Earth), 2.0f},
                {(MagicSystem.DamageType.Fire, Enemy.Enemy.ElementTypes.Air), 1.0f},
                
                {(MagicSystem.DamageType.Water, Enemy.Enemy.ElementTypes.Fire), 2.0f},
                {(MagicSystem.DamageType.Water, Enemy.Enemy.ElementTypes.Water), 1.0f},
                {(MagicSystem.DamageType.Water, Enemy.Enemy.ElementTypes.Earth), 1.0f},
                {(MagicSystem.DamageType.Water, Enemy.Enemy.ElementTypes.Air), 0.5f},
                
                {(MagicSystem.DamageType.Earth, Enemy.Enemy.ElementTypes.Fire), 0.5f},
                {(MagicSystem.DamageType.Earth, Enemy.Enemy.ElementTypes.Water), 1.0f},
                {(MagicSystem.DamageType.Earth, Enemy.Enemy.ElementTypes.Earth), 1.0f},
                {(MagicSystem.DamageType.Earth, Enemy.Enemy.ElementTypes.Air), 2.0f},
                
                {(MagicSystem.DamageType.Air, Enemy.Enemy.ElementTypes.Fire), 1.0f},
                {(MagicSystem.DamageType.Air, Enemy.Enemy.ElementTypes.Water), 2.0f},
                {(MagicSystem.DamageType.Air, Enemy.Enemy.ElementTypes.Earth), 0.5f},
                {(MagicSystem.DamageType.Air, Enemy.Enemy.ElementTypes.Air), 1.0f},
            };
        
        
        private void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
            _magicSystem = GetComponent<MagicSystem>();
            sphereCollider.radius = trackingRange;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!_hasCollided && layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                if (!isFirstTarget)
                {
                    isFirstTarget = true;
                    target = other.gameObject;
                    enemy = target.GetComponent<Enemy.Enemy>();
                }
            }
        }

        private void TrackEnemy(GameObject newTarget)
        {
            if (Vector3.Distance(newTarget.transform.position, transform.position) < sphereCollider.radius * 2.0f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(newTarget.transform.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, trackingSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * (trackingSpeed * Time.deltaTime));
            }

            if (Vector3.Distance(newTarget.transform.position, transform.position) < 5.0f)
            {
                // Get the spell that I am currently casting
                Debug.Log("First Target: " + newTarget.name + " has been hit for " + _magicSystem.damage + " damage");
                
                // Deal damage based on type of spell

                if (_damageMultipliers.TryGetValue((_magicSystem.damageType, enemy.elementType), out float damageMultiplier))
                {
                    enemy.TakeDamage(_magicSystem.damage * damageMultiplier);
                }
                
                // GameObject explosion = Instantiate(_magicSystem.explosionPrefab, transform.position, Quaternion.identity);
                // Instantiate explosion prefab and rotate it in the direction of the target
                GameObject explosion = Instantiate(_magicSystem.explosionPrefab, transform.position, Quaternion.LookRotation(newTarget.transform.position - transform.position));
                Destroy(explosion, 2.0f);
                Destroy(gameObject);
            }
            
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, sphereCollider.radius);
        }

        private void Update()
        {
            if (isFirstTarget && target != null)
            {
                TrackEnemy(target);
            }
        }
    }
}
