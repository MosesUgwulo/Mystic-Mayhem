using Spells;
using UnityEngine;

namespace Enemy.Enemy_Spells
{
    public class EnemyAirball : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            var enemyAirball = GetInstanceOfPrefab(prefab, go.transform, this);
            enemyAirball.GetComponent<Rigidbody>().AddForce(go.transform.forward * speed, ForceMode.Impulse);
            Destroy(enemyAirball, lifeTime);
        }
    }
}
