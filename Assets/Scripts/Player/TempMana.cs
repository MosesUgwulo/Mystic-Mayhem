using UnityEngine;

namespace Player
{
    public class TempMana : MonoBehaviour
    {
    
        void Start()
        {
        
        }

    
        void Update()
        {
            if (Input.GetKey(KeyCode.F))
            {
                ManaBar.Mana -= 10f;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                ManaBar.Mana += 1f;
            }
        }
    }
}
