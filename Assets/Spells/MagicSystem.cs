using System;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Spells
{
    public abstract class MagicSystem : MonoBehaviour
    {
        public string id;
        public string[] phrases;
        public GameObject prefab;
        public float manaCost;
        public float cooldown;
        public float damage;
        public float speed;
        public float timer;
        public bool CanCast => timer >= cooldown;
        
        public abstract void CastSpell();
        
        private void Start()
        {
            timer = cooldown;
        }
        
        private void Update()
        {
            if (timer < cooldown)
            {
                timer += Time.deltaTime;
            }
        }
        
        // start cooldown
        public void StartCooldown()
        {
            timer = 0;
        }
    }
}
