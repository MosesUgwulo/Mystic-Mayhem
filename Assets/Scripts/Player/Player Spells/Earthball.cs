using Player;
using UnityEngine;

namespace Spells
{
    public class EarthBall : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            if (!CanCast) return;
            
            Debug.Log("Casting Earthball");
            ManaBar.Mana -= manaCost;
            var earthball = GetInstanceOfPrefab(prefab, cam.transform, this); // Get the instance of the prefab and set it's parent to the camera
            earthball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse); // Add force to the earthball in the direction of the camera
            
            StartCooldown();
            Destroy(earthball, lifeTime);
        }
        
    }
}
