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
using UnityEngine.Serialization;
using Ping = System.Net.NetworkInformation.Ping;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;
    public SpeechRecognition speechRecognition;
    public SpeechRecognitionAPI speechRecognitionAPI;
    public MagicSystem magicSystem;
    public GameObject player;
    public TMP_Text connectedText;
    private float _timer;
    private float _timeToPing = 30f;
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
        speechRecognition = GameObject.Find("Speech").GetComponent<SpeechRecognition>();
        speechRecognitionAPI = GameObject.Find("HuggingFaceSpeechAPI").GetComponent<SpeechRecognitionAPI>();
        
        if (IsInternetConnected())
        {
            speechRecognitionAPI.enabled = true;
            speechRecognition.enabled = false;
        }
        
        if (!IsInternetConnected())
        {
            speechRecognitionAPI.enabled = false;
            speechRecognition.enabled = true;
        }
        
        connectedText.text = IsInternetConnected() ? "Connected" : "Not Connected";
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
        // Ping every 30 seconds to check if the internet is connected or not and switch the speech recognition system accordingly
        _timer += Time.deltaTime;
        if (_timer > _timeToPing)
        {
            Debug.Log("Pinging");
            _timer = 0;
            if (IsInternetConnected())
            {
                speechRecognitionAPI.enabled = true;
                speechRecognition.enabled = false;
                connectedText.text = "Connected";
            }
            else
            {
                speechRecognitionAPI.enabled = false;
                speechRecognition.enabled = true;
                connectedText.text = "Not Connected";
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
