using Spells;
using UnityEngine;

namespace Enemy.Enemy_Spells
{
    public class EnemyFireball : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            var enemyFireball = GetInstanceOfPrefab(prefab, go.transform, this);
            enemyFireball.GetComponent<Rigidbody>().AddForce(go.transform.forward * speed, ForceMode.Impulse);
            Destroy(enemyFireball, lifeTime);
        }
    }
}
