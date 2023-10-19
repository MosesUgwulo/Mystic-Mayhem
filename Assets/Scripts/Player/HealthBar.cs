using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HealthBar : MonoBehaviour
    {
        private const float MaxHealth = 200f;
        [Range(0,MaxHealth)]
        private static float _health = MaxHealth;
        private static Image _healthBarImg;
        
        void Start()
        {
            _healthBarImg = GetComponent<Image>();
        }

        
        void Update()
        {
            
        }
        
        public static float Health
        {
            get => _health;
            set
            {
                // Clamp health between 0 and MaxHealth
                _health = Math.Clamp(value, 0, MaxHealth);
                
                // Update health UI
                var healthPercentage = _health / MaxHealth;
                _healthBarImg.fillAmount = healthPercentage;
                
                _healthBarImg.color = new Color(23f / 255f, 108f / 255f, 12f / 255f);
                
                if (healthPercentage < 0.5f)
                {
                    _healthBarImg.color = Color.yellow;
                }
                if (healthPercentage < 0.25f)
                {
                    _healthBarImg.color = Color.red;
                }
                
                // If health is 0, kill the player
                if (_health <= 0)
                {
                    //TODO: Kill the player
                    Debug.Log("Player is dead!");
                }
            }
        }
    }
}
