using Spells;
using UnityEngine;

namespace Player.Player_Spells
{
    public class CounterSpell : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            if (!CanCast) return;
            
            Debug.Log("Casting CounterSpell");
            ManaBar.Mana -= manaCost;
            Collider[] spellColliders = Physics.OverlapSphere(cam.transform.position, 50f);
            
            foreach (var spellCollider in spellColliders)
            {
                if (spellCollider.CompareTag("Spell"))
                {
                    Destroy(spellCollider.gameObject);
                }
            }
        }
    }
}
