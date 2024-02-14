using System.Collections.Generic;
using Spells;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public List<MagicSystem> spells;
        public float health;
        public float walkSpeed;
        public Transform castingPoint;
        public Transform player;
        public LayerMask groundMask, playerMask;
        protected NavMeshAgent agent;
        
        // Patrolling Variables
        public float patrolRange;
        protected Vector3 patrolTarget;
        protected bool isPatrolTargetSet;
        
        // Other Variables
        public float detectionRange, attackRange;
        protected bool isPlayerInRange, isPlayerInAttackRange;

        public abstract void SetPatrolTarget();
        public abstract void Patrolling();
        public abstract void Chasing();
        public abstract void Attack();
    }
}
