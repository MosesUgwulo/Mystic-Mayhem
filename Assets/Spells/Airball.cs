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
            var airball = Instantiate(prefab, cam.transform.position + cam.transform.forward, cam.transform.rotation);
            airball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
            
            StartCooldown();
            Destroy(airball, lifeTime);
        }
    }
}
