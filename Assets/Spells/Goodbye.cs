using UnityEngine;

namespace Spells
{
    public class Goodbye : MagicSystem
    {
        public override void CastSpell(GameObject go = null)
        {
            Debug.Log("Exiting Game");
            Application.Quit();
        }
        
    }
}
