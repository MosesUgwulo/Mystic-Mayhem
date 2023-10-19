using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class ManaBar : MonoBehaviour
    {
        private const float MaxMana = 200f;
        [Range(0, MaxMana)]
        private static float _mana = MaxMana;

        private static Image _manaBarImg;
        void Start()
        {
            _manaBarImg = GetComponent<Image>();
        }

        
        void Update()
        {
            
        }
        
        public static float Mana
        {
            get => _mana;
            set
            {
                _mana = Mathf.Clamp(value, 0, MaxMana);
                
                var manaPercentage = _mana / MaxMana;
                _manaBarImg.fillAmount = manaPercentage;

                _manaBarImg.color = new Color(0, 1, 1, manaPercentage);
                
            }
        }
        
    }
}
