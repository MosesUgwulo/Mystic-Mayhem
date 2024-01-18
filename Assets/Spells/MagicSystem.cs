using System;
using Player;
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
        public float lifeTime;
        public bool hasLearned;
        public bool CanCast
        {
            get { return timer >= cooldown && ManaBar.Mana >= manaCost; }
        }

        protected GameObject cam;
        
        public enum DamageType
        {
            Fire,
            Water,
            Earth,
            Air
        }
        
        public abstract void CastSpell();
        
        private void Start()
        {
            timer = cooldown;
            cam = GameManager.instance.player.GetComponentInChildren<Camera>().gameObject;
            
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
