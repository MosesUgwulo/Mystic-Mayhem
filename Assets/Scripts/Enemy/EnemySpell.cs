// using UnityEngine;
//
// namespace Enemy
// {
//     public abstract class EnemySpell : MonoBehaviour
//     {
//         public DamageType damageType;
//         public GameObject prefab;
//         public float damage;
//         public float manaCost;
//         public float speed;
//         public float cooldown;
//         public float lifetime;
//         public float timer;
//         protected bool CanCast => timer >= cooldown;
//         public enum DamageType
//         {
//             Fire,
//             Water,
//             Earth,
//             Air
//         }
//         
//         protected static GameObject GetInstanceOfPrefab<T>(GameObject prefab, Transform castingPoint, T original) where T : EnemySpell
//         {
//             var fab = Instantiate(prefab, castingPoint.position + castingPoint.forward, castingPoint.rotation);
//             var newScript = fab.AddComponent<T>();
//             var fields = typeof(T).GetFields();
//             
//             foreach (var field in fields)
//             {
//                 field.SetValue(newScript, field.GetValue(original));
//             }
//         
//             return fab;
//         }
//         
//         
//         void Start()
//         {
//             timer = cooldown;
//         }
//
//         void Update()
//         {
//             if (timer < cooldown)
//             {
//                 timer += Time.deltaTime;
//             }
//         }
//         
//         protected void StartCooldown()
//         {
//             timer = 0;
//         }
//     }
// }
