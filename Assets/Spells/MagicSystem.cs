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

        protected bool CanCast => timer >= cooldown && ManaBar.Mana >= manaCost;

        protected GameObject cam;
        
        protected static GameObject GetInstanceOfPrefab<T>(GameObject prefab, GameObject cam, T original) where T : MagicSystem
        {
            var fab = Instantiate(prefab, cam.transform.position + cam.transform.forward, cam.transform.rotation);
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
        protected void StartCooldown()
        {
            timer = 0;
        }
    }
}
