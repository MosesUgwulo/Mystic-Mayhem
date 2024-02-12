using System.Collections.Generic;
using System.Linq;
using Spells;
using UnityEngine;

namespace Enemy
{
    public class Water : Enemy
    {
        private List<MagicSystem> _spells;
        public float timer;
        void Start()
        {
            var spellsArray = spells.GetComponents<MagicSystem>();
            _spells = spellsArray.ToList();
            spells = _spells.FirstOrDefault(s => s.phrases.Contains("Splash"));

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

        public override void setPatrolTarget()
        {
            throw new System.NotImplementedException();
        }

        public override void Patrolling()
        {
            throw new System.NotImplementedException();
        }

        public override void Chasing()
        {
            throw new System.NotImplementedException();
        }

        public override void Attack()
        {
            if (spells == null)
            {
                Debug.Log("No spell found");
                return;
            }
            
            mana -= spells.manaCost;
            var waterball = Instantiate(spells.prefab, castingPoint.position + castingPoint.forward, castingPoint.rotation);
            waterball.GetComponent<Rigidbody>().AddForce(castingPoint.forward * spells.speed, ForceMode.Impulse);
            
            Destroy(waterball, spells.lifeTime);
        }
    }
}
