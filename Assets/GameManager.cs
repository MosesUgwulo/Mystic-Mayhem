using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Enemy;
using Player;
using Spells;
using TMPro;
using UnityEngine;
using Ping = System.Net.NetworkInformation.Ping;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public MagicSystem magicSystem;
    public GameObject player;
    public TMP_Text textMeshPro;
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
        
        if (IsInternetConnected())
        {
            GameObject.Find("HuggingFaceSpeechAPI").GetComponent<SpeechRecognitionAPI>().enabled = true;
            GameObject.Find("Speech").GetComponent<SpeechRecognition>().enabled = false;
        }
        
        if (!IsInternetConnected())
        {
            GameObject.Find("HuggingFaceSpeechAPI").GetComponent<SpeechRecognitionAPI>().enabled = false;
            GameObject.Find("Speech").GetComponent<SpeechRecognition>().enabled = true;
        }
        
        textMeshPro.text = IsInternetConnected() ? "Connected" : "Not Connected";
    }

    private static bool IsInternetConnected()
    {
        try
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send("8.8.8.8", 3000);
            
            if (reply != null && reply.Status == IPStatus.Success)
            {
                Debug.Log("Internet is connected");
                return true;
            }
            else
            {
                Debug.Log("Internet is not connected");
                return false;
            }
        } catch (Exception e)
        {
            Debug.Log("Error: " + e.Message);
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
