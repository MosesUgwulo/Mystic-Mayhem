using System;
using System.Collections.Generic;
using System.Linq;
using Spells;
using UnityEngine;
using UnityEngine.Windows.Speech;

namespace Player
{
    public class SpeechRecognition : MonoBehaviour
    {
        private List<MagicSystem> _spells;
        public KeywordRecognizer keywordRecognizer;
        
        private void Awake()
        {
            // Get the MagicSystem game object from the parent of this game object.
            GameObject magicSystem = GameObject.Find("MagicSystem");
            
            // Get all the Spell components in MagicSystem game object.
            var spellsArray = magicSystem.GetComponents<MagicSystem>();
            _spells = spellsArray.ToList();
        }
        
        private void Start()
        {
            // Check if the platform supports speech recognition.
            Debug.Log("Speech Recognition Supported: <b>" + PhraseRecognitionSystem.isSupported + "</b>");
            if (!PhraseRecognitionSystem.isSupported)
                return;
        
            // Subscribe to events.
            PhraseRecognitionSystem.OnStatusChanged += (status) => Debug.Log("Status: <b>" + status + "</b>");
            PhraseRecognitionSystem.OnError += (error) => Debug.LogError("Error: <b>" + error + "</b>");
        
            // Create a new keyword recognizer.
            keywordRecognizer = new KeywordRecognizer(_spells.SelectMany(ms => ms.phrases).ToArray());
            keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        
            keywordRecognizer.Start();
        
            // Start recognition.
            PhraseRecognitionSystem.Restart();
        
        
        }

        private void OnDestroy()
        {
            if (keywordRecognizer != null && keywordRecognizer.IsRunning)
                keywordRecognizer.Stop();
        }
    
        private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                Debug.Log("You said: <b>" + args.text + "</b> - confidence: <b>" + args.confidence + "</b>");
            
                // Get the spell that matches the recognized phrase.
                MagicSystem spell = _spells.FirstOrDefault(s => s.phrases.Contains(args.text));
                if (spell == null)
                    return;
            
                // Cast the spell.
                spell.CastSpell();
            }
        }
        
    }
}
