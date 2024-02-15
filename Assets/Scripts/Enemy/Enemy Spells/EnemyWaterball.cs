using Spells;
using UnityEngine;

namespace Enemy.Enemy_Spells
{
    public class EnemyWaterball : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            var enemyWaterball = GetInstanceOfPrefab(prefab, go.transform, this);
            enemyWaterball.GetComponent<Rigidbody>().AddForce(go.transform.forward * speed, ForceMode.Impulse);
            Destroy(enemyWaterball, lifeTime);
        }
    }
}
