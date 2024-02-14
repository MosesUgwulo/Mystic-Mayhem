using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Spells
{
    public class Fireball : MagicSystem
    {
        
        public override void CastSpell(GameObject go = null)
        {
                if (!CanCast) return;
            
                Debug.Log("Casting Fireball");
                ManaBar.Mana -= manaCost;
                var fireball = GetInstanceOfPrefab(prefab, cam.transform, this);
                fireball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse);
            
                StartCooldown();
                Destroy(fireball, lifeTime);
        }
        
    }
}
