using Audio;
using Spells;
using UnityEngine;

namespace Terrain
{
    public class VillageFX : MonoBehaviour
    {
        public string biomeName; // Needs to match the name of the sound in the AudioManager
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                AudioManager.instance.PlayAmbient(biomeName);
            }
        }
    }
}
