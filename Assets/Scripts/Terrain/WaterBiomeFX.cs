using Audio;
using UnityEngine;

namespace Terrain
{
    public class WaterBiomeFX : MonoBehaviour
    {
        public string biomeName; // Needs to match the name of the sound in the AudioManager
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!AudioManager.instance.IsPlaying(biomeName))
                {
                    AudioManager.instance.PlayAmbient(biomeName);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AudioManager.instance.TryPause(biomeName);
            }
        }
    }
}
