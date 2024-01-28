using System;
using UnityEngine;

namespace Spells
{
    public class SpellTracking : MonoBehaviour
    {
        public LayerMask layerMask;
        public SphereCollider sphereCollider;
        
        public float trackingSpeed = 0.01f;
        void Start()
        {
            sphereCollider = GetComponent<SphereCollider>();
        }

        void Update()
        {
        
        }

        private void OnTriggerEnter(Collider other)
        {
            if (layerMask == (layerMask | (1 << other.gameObject.layer)))
            {
                Debug.Log("Tracking");
                TrackEnemy(other.transform);
            }
        }

        private void TrackEnemy(Transform otherGameObject)
        {
            if (Vector3.Distance(otherGameObject.position, transform.position) < sphereCollider.radius)
            {
                Quaternion targetRotation = Quaternion.LookRotation(otherGameObject.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, trackingSpeed * Time.deltaTime);
                transform.Translate(Vector3.forward * (trackingSpeed * Time.deltaTime));
            }
        }
    }
}
