using Player;
using UnityEngine;

namespace Spells
{
    public class Airball : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            if (!CanCast) return;
            
            Debug.Log("Casting Airball");
            ManaBar.Mana -= manaCost;
            var airball = GetInstanceOfPrefab(prefab, cam.transform, this); // Get the instance of the prefab and set it's parent to the camera
            airball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse); // Add force to the airball in the direction of the camera
            
            StartCooldown();
            Destroy(airball, lifeTime);
        }
        
    }
}
