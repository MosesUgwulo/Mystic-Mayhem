using Spells;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemy
{
    public abstract class Enemy : MonoBehaviour
    {
        public float health;
        public float speed;
        public float damage;
        public float mana;
        public MagicSystem spells;
        public Transform castingPoint;
        
        public abstract void Attack();
        
        
    }
}
