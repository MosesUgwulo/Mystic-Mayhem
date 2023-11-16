using Player;
using UnityEngine;

namespace Spells
{
    public class Fireball : MagicSystem
    {
        
        public override void CastSpell()
        {
            if (!CanCast) return;
            
            Debug.Log("Casting Fireball");
            ManaBar.Mana -= manaCost;
            
            StartCooldown();
        }
    }
}
