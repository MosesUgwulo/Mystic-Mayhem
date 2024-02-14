using System;
using Enemy;
using Player;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Spells
{
    public abstract class MagicSystem : MonoBehaviour
    {
        public DamageType damageType;
        public string[] phrases;
        public GameObject prefab;
        public float manaCost;
        public float cooldown;
        public float damage;
        public float speed;
        public float timer;
        public float lifeTime;
        public bool hasLearned;
        public bool isEnemySpell;
        
        protected bool CanCast => timer >= cooldown && ManaBar.Mana >= manaCost;

        protected GameObject cam;
        
        protected static GameObject GetInstanceOfPrefab<T>(GameObject prefab, Transform origin, T original) where T : MagicSystem
        {
            var fab = Instantiate(prefab, origin.position + origin.forward, origin.rotation);
            var newScript = fab.AddComponent<T>();
            var fields = typeof(T).GetFields();
            
            foreach (var field in fields)
            {
                field.SetValue(newScript, field.GetValue(original));
            }

            return fab;
        }
        
        public enum DamageType
        {
            Fire,
            Water,
            Earth,
            Air,
            Utility,
            None,
        }
        
        public abstract void CastSpell(GameObject go = null); // go is the GameObject casting the spell
        
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
        protected void StartCooldown()
        {
            timer = 0;
        }
    }
}
