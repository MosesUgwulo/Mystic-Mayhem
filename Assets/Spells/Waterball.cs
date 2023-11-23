using Player;
using UnityEngine;

namespace Spells
{
    public class Waterball : MagicSystem
    {
        public override void CastSpell()
        {
            if (!CanCast) return;
            
            Debug.Log("Casting Waterball");
            ManaBar.Mana -= manaCost;
            var waterball = Instantiate(prefab, cam.transform.position + cam.transform.forward, cam.transform.rotation);
            waterball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
            
            StartCooldown();
            Destroy(waterball, lifeTime);
        }
    }
}
