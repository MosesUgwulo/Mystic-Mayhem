using Spells;
using UnityEngine;

namespace Enemy.Enemy_Spells
{
    public class EnemyEarthball : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            var enemyEarthball = GetInstanceOfPrefab(prefab, go.transform, this);
            enemyEarthball.GetComponent<Rigidbody>().AddForce(go.transform.forward * speed, ForceMode.Impulse);
            Destroy(enemyEarthball, lifeTime);
        }
    }
}
