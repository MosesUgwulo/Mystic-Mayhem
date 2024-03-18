using System.Collections.Generic;
using System.Linq;
using Spells;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class AirEnemy : Enemy
    {
        private float _timer; // Timer for attack cooldown and so it doesn't mess with the other enemies cooldowns
        void Start()
        {
            GameObject magicSystem = GameObject.Find("MagicSystem"); 
            
            var spellArray = magicSystem.GetComponents<MagicSystem>(); // Get all the spells from the MagicSystem and put them in an array
            spells = spellArray.ToList().FindAll(s => s.damageType == MagicSystem.DamageType.Air && s.isEnemySpell); // Find all the spells that are of the Air type and are for the enemy
            
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            agent.speed = walkSpeed;
            anim = GetComponent<Animator>();

            if (anim == null) anim = GetComponent<Animator>();
        }

        public override void SetPatrolTarget()
        {
            float randomX = Random.Range(-patrolRange, patrolRange);
            float randomZ = Random.Range(-patrolRange, patrolRange);

            var position = transform.position;
            patrolTarget = new Vector3(position.x + randomX, position.y, position.z + randomZ);

            if (Physics.Raycast(patrolTarget, -transform.up, 2f, groundMask)) // Make sure the target is on the ground and not in the air
            {
                isPatrolTargetSet = true;
            }
        }

        public override void Patrolling()
        {
            if (!isPatrolTargetSet) SetPatrolTarget();
            
            if (isPatrolTargetSet) agent.SetDestination(patrolTarget);
            
            var distance = transform.position - patrolTarget;

            if (distance.magnitude < 1f) isPatrolTargetSet = false;
        }

        public override void Chasing()
        {
            agent.SetDestination(player.position);
        }

        public override void Attack()
        {
            agent.SetDestination(transform.position); // Stop the enemy from moving
            transform.LookAt(player); // Look at the player so the enemy can cast the spell
            
            var spell = spells[Random.Range(0, spells.Count)];
            spell.CastSpell(castingPoint.gameObject);
            _timer = spell.cooldown;
            
            anim.Play("Attack", 0, 0f);
            anim.SetBool(IsChasing, false);
            anim.SetTrigger(CastingSpellB);
        }

        public override void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0) Destroy(gameObject);
        }

        void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            
            var position = transform.position;
            isPlayerInRange = Physics.CheckSphere(position, detectionRange, playerMask);
            isPlayerInAttackRange = Physics.CheckSphere(position, attackRange, playerMask);
            
            if (!isPlayerInRange && !isPlayerInAttackRange)
            {
                agent.isStopped = false;
                anim.SetBool(CanSee, false);
                Patrolling();
                anim.SetBool(IsPatrolling, true);
            }

            if (isPlayerInRange && !isPlayerInAttackRange)
            {
                agent.isStopped = false;
                anim.SetBool(IsPatrolling, false);
                Chasing();
                anim.SetBool(CanSee, true);
                anim.SetBool(IsChasing, true);
            }
            
            if (isPlayerInAttackRange && _timer <= 0) Attack();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, patrolRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
