using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Enemy;
using Player;
using Spells;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public MagicSystem magicSystem;
    public GameObject player;
    // public SpeechRecognition speechRecognition;
    public SpeechRecognitionAPI speechRecognitionAPI;
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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
