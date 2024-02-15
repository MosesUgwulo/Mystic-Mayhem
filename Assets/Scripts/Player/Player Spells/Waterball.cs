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
            var waterball = GetInstanceOfPrefab(prefab, cam.transform, this); // Get the instance of the prefab and set it's parent to the camera
            waterball.GetComponent<Rigidbody>().AddForce(cam.transform.forward * speed, ForceMode.Impulse); // Add force to the waterball in the direction of the camera
            
            StartCooldown();
            Destroy(waterball, lifeTime);
        }
    }
}
