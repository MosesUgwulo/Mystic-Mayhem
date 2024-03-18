using System.Collections.Generic;
using Spells;
using UnityEngine;

namespace Player
{
    public class LearnSpell : MonoBehaviour
    {
        public List<string> spellNames;
        public List<MagicSystem> spells;
        void Start()
        {
            // Get all of the spell names from the MagicSystem class
            foreach(var spell in GameManager.instance.magicSystem.GetComponents<MagicSystem>())
            {
                if (spell.spellName == "") continue;
                spellNames.Add(spell.spellName);
                spells.Add(spell);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Pick a random spell to learn
                var random = new System.Random();
                var index = random.Next(0, spellNames.Count);
                var spell = spells[index];
                spell.hasLearned = true;
                Debug.Log($"You have learned the spell {spell.spellName}");
                Destroy(gameObject);
            }
        }
    }
}
