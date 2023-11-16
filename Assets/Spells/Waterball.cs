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
            
            StartCooldown();
        }
    }
}
