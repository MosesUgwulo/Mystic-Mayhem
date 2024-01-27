using System.Collections.Generic;
using System.Linq;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class Fire : Enemy
    {
        private List<MagicSystem> _spells;
        public float timer;
        void Start()
        {
            var spellsArray = spells.GetComponents<MagicSystem>();
            _spells = spellsArray.ToList();
            spells = _spells.FirstOrDefault(s => s.phrases.Contains("Flame"));

            if (spells == null)
            {
                return;
            }
            
            timer = spells.cooldown;
        }

        public override void Attack()
        {

            if (spells == null)
            {
                Debug.Log("No spell found");
                return;
            }
            
            mana -= spells.manaCost;
            var fireball = Instantiate(spells.prefab, castingPoint.position + castingPoint.forward, castingPoint.rotation);
            fireball.GetComponent<Rigidbody>().AddForce(castingPoint.forward * spells.speed, ForceMode.Impulse);
            
            Destroy(fireball, spells.lifeTime);
        }
        
        private void Update()
        {
            if (timer < spells.cooldown)
            {
                timer += Time.deltaTime;
            }
            
            if (Input.GetKeyDown(KeyCode.Q) && timer >= spells.cooldown)
            {
                Attack();
                timer = 0;
            }
            
        }
        
    }
}
