using UnityEngine;

namespace Spells
{
    public class Goodbye : MagicSystem
    {
        public override void CastSpell()
        {
            Application.Quit();
        }
    }
}
