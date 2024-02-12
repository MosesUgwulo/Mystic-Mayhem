using Player;
using UnityEngine;

namespace Spells
{
    public class EarthBall : MagicSystem
    {
        public override void CastSpell()
        {
            if (!CanCast) return;
            
            Debug.Log("Casting Earthball");
            ManaBar.Mana -= manaCost;
            var earthball = GetInstanceOfPrefab(prefab, cam, this);
            earthball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
            
            StartCooldown();
            Destroy(earthball, lifeTime);
        }
        
    }
}
