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
        public ElementTypes elementType;
        public float health;
        public float walkSpeed;
        public Transform castingPoint;
        public Transform player;
        public LayerMask groundMask, playerMask;
        public Animator anim;
        protected NavMeshAgent agent;
        protected static readonly int IsPatrolling = Animator.StringToHash("IsPatrolling");
        protected static readonly int CanSee = Animator.StringToHash("CanSee");
        protected static readonly int CastingSpellT = Animator.StringToHash("CastingSpellT");
        protected static readonly int CastingSpellB = Animator.StringToHash("CastingSpellB");
        protected static readonly int Cooldown = Animator.StringToHash("Cooldown");
        protected static readonly int IsCasting = Animator.StringToHash("IsCasting");
        protected static readonly int IsChasing = Animator.StringToHash("IsChasing");
        
        // Patrolling Variables
        public float patrolRange;
        protected Vector3 patrolTarget;
        protected bool isPatrolTargetSet;
        
        // Other Variables
        public float detectionRange, attackRange;
        protected bool isPlayerInRange, isPlayerInAttackRange;

        public enum ElementTypes
        {
            Fire,
            Water,
            Earth,
            Air,
        }

        public abstract void SetPatrolTarget(); // Set the patrol target for the enemy if it's not set yet
        public abstract void Patrolling(); // If the enemy is patrolling, set the target and move towards it
        public abstract void Chasing(); // If the enemy is chasing the player, set the destination to the player's position
        public abstract void Attack(); // If the enemy is in attack range, stop the enemy from moving, look at the player and cast a spell
        public abstract void TakeDamage(float damage); // If the enemy takes damage, subtract the damage from the health and if the health is less than or equal to 0, destroy the enemy
    }
}
