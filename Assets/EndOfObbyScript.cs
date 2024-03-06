using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndOfObbyScript : MonoBehaviour
{
    public TMPro.TMP_Text text;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text.text = "Congratulations! You have completed the obby!";
        }
    }
}
