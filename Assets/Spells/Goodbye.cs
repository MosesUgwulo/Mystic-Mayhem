using UnityEngine;

namespace Spells
{
    public class Goodbye : MagicSystem
    {
        public override void CastSpell()
        {
            Debug.Log("Exiting Game");
            Application.Quit();
        }
        
    }
}
