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
            var fireball = GetInstanceOfPrefab(prefab, cam.transform, this); // Get the instance of the prefab and set it's parent to the camera
            fireball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse); // Add force to the fireball in the direction of the camera
        
            StartCooldown();
            Destroy(fireball, lifeTime);
        }
        
    }
}
