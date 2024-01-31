using Player;
using UnityEngine;

namespace Spells
{
    public class Airball : MagicSystem
    {
        
        public override void CastSpell()
        {
            if (!CanCast) return;
            
            Debug.Log("Casting Airball");
            ManaBar.Mana -= manaCost;
            var airball = GetInstanceOfPrefab(prefab, cam, this);
            airball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
            
            StartCooldown();
            Destroy(airball, lifeTime);
        }
        
    }
}
