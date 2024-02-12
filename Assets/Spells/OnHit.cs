using System;
using Enemy;
using Player;
using UnityEngine;

namespace Spells
{
    public class OnHit : MonoBehaviour
    {
        
        private bool _hasCollided = false;
        public LayerMask layerMask;
        public float trackingSpeed = 10.0f;
        public GameObject target;
        public Enemy.Enemy enemy;
        public bool isFirstTarget = false;
        public SphereCollider sphereCollider;
        private MagicSystem _magicSystem;
        
        
        private void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
            _magicSystem = GetComponent<MagicSystem>();
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
            Debug.Log("Tracking: " + newTarget.name);
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
                // Destroy(gameObject);
                // Destroy(newTarget);
                
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
