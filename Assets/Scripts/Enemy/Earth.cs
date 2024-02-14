// using System.Collections.Generic;
// using System.Linq;
// using Spells;
// using UnityEngine;
// using UnityEngine.AI;
//
// namespace Enemy
// {
//     public class Earth : Enemy
//     {
//         private List<MagicSystem> _spells;
//         public float timer;
//         void Start()
//         {
//             var spellsArray = spells.GetComponents<MagicSystem>();
//             _spells = spellsArray.ToList();
//             spells = _spells.FirstOrDefault(s => s.phrases.Contains("Terra"));
//
//             if (spells == null)
//             {
//                 return;
//             }
//             
//             timer = spells.cooldown;
//             
//             player = GameObject.Find("Player").transform;
//             agent = GetComponent<NavMeshAgent>();
//         }
//
//         public override void SetPatrolTarget()
//         {
//             float randomX = Random.Range(-patrolRange, patrolRange);
//             float randomZ = Random.Range(-patrolRange, patrolRange);
//
//             var position = transform.position;
//             patrolTarget = new Vector3(position.x + randomX, position.y, position.z + randomZ);
//
//             if (Physics.Raycast(patrolTarget, -transform.up, 2f, groundMask)) 
//             {
//                 isPatrolTargetSet = true;
//             }
//         }
//
//         public override void Patrolling()
//         {
//             if (!isPatrolTargetSet) SetPatrolTarget();
//             
//             if (isPatrolTargetSet) agent.SetDestination(patrolTarget);
//             
//             var distance = transform.position - patrolTarget;
//
//             if (distance.magnitude < 1f) isPatrolTargetSet = false;
//         }
//
//         public override void Chasing()
//         {
//             agent.SetDestination(player.position);
//         }
//
//         public override void Attack()
//         {
//             timer = 0;
//             agent.SetDestination(transform.position);
//             transform.LookAt(player);
//             
//             if (spells == null)
//             {
//                 Debug.Log("No spell found");
//                 return;
//             }
//             
//             var earthball = Instantiate(spells.prefab, castingPoint.position + castingPoint.forward, castingPoint.rotation);
//             earthball.GetComponent<Rigidbody>().AddForce(castingPoint.forward * spells.speed, ForceMode.Impulse);
//             
//             Destroy(earthball, spells.lifeTime);
//         }
//         
//         void Update()
//         {
//             if (timer < spells.cooldown)
//             {
//                 timer += Time.deltaTime;
//             }
//             
//             var position = transform.position;
//             isPlayerInRange = Physics.CheckSphere(position, detectionRange, playerMask);
//             isPlayerInAttackRange = Physics.CheckSphere(position, attackRange, playerMask);
//             
//             if (!isPlayerInRange && !isPlayerInAttackRange) Patrolling();
//             if (isPlayerInRange && !isPlayerInAttackRange) Chasing();
//             if (isPlayerInAttackRange && timer >= spells.cooldown) Attack();
//         }
//         
//         private void OnDrawGizmosSelected()
//         {
//             Gizmos.color = Color.green;
//             Gizmos.DrawWireSphere(transform.position, patrolRange);
//             Gizmos.color = Color.yellow;
//             Gizmos.DrawWireSphere(transform.position, detectionRange);
//             Gizmos.color = Color.red;
//             Gizmos.DrawWireSphere(transform.position, attackRange);
//         }
//     }
// }
