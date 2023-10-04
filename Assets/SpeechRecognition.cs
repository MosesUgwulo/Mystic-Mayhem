using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class SpeechRecognition : MonoBehaviour
{
    public String[] words = new String[] { "Test", "Hello", "Goodbye" };
    public KeywordRecognizer keywordRecognizer;
    public GameObject block;
    
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
        keywordRecognizer = new KeywordRecognizer(words);
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
        Debug.Log("You said: <b>" + args.text + "</b> - confidence: <b>" + args.confidence + "</b>");
        if (args.text == "Spawn block")
        {
            Instantiate(block, new Vector3(0, 10, 0), Quaternion.identity);
        }

        if (args.text == "Goodbye")
        {
            Application.Quit();
        }
    }
}
