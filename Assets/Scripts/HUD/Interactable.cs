using UnityEngine;

namespace HUD
{
    public class Interactable : MonoBehaviour
    {
        public Dialogue dialogue;

        public void Interact()
        {
            DialogueManager.instance.StartDialogue(dialogue);
        }
    }
}
