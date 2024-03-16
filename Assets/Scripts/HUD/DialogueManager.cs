using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HUD
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager instance;
        public TMP_Text nameText;
        public TMP_Text dialogueText;
        public Animator animator;
        private Queue<string> _sentences;
        private static readonly int IsOpen = Animator.StringToHash("IsOpen");

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject); 
            }
        }

        void Start()
        {
            _sentences = new Queue<string>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            animator.SetBool(IsOpen, true);
            
            nameText.text = dialogue.name;
            
            _sentences.Clear();
            
            foreach (var sentence in dialogue.sentences)
            {
                _sentences.Enqueue(sentence);
            }
            
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if (_sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            
            var sentence = _sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
        
        IEnumerator TypeSentence(string sentence)
        {
            dialogueText.text = "";
            foreach (var letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return 3f;
            }
        }

        public void EndDialogue()
        {
            animator.SetBool(IsOpen, false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DisplayNextSentence();
            }
        }
    }
}
