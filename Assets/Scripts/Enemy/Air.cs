using System.Collections.Generic;
using System.Linq;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class Air : Enemy
    {
        private List<MagicSystem> _spells;
        public float timer;
        void Start()
        {
            var spellsArray = spells.GetComponents<MagicSystem>();
            _spells = spellsArray.ToList();
            spells = _spells.FirstOrDefault(s => s.phrases.Contains("Zephyr"));

            if (spells == null)
            {
                return;
            }
            
            timer = spells.cooldown;
        }

        void Update()
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

        public override void Attack()
        {
            if (spells == null)
            {
                Debug.Log("No spell found");
                return;
            }
            
            mana -= spells.manaCost;
            var airball = Instantiate(spells.prefab, castingPoint.position + castingPoint.forward, castingPoint.rotation);
            airball.GetComponent<Rigidbody>().AddForce(castingPoint.forward * spells.speed, ForceMode.Impulse);
            
            Destroy(airball, spells.lifeTime);
        }
    }
}
