using Player;
using UnityEngine;

namespace Spells
{
    public class Waterball : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            if (!CanCast) return;
            
            Debug.Log("Casting Waterball");
            ManaBar.Mana -= manaCost;
            var waterball = GetInstanceOfPrefab(prefab, cam.transform, this);
            waterball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
            
            StartCooldown();
            Destroy(waterball, lifeTime);
        }
    }
}
