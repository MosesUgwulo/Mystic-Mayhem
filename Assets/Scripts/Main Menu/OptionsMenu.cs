using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Main_Menu
{
    public class OptionsMenu : MonoBehaviour
    {
        public TMP_Dropdown micDropdown;

        private void Awake()
        {
            micDropdown = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();
        }

        void Start()
        {
            PopulateMicDropdown();
        }

        private void PopulateMicDropdown()
        {
            List<string> micOptions = new List<string>(Microphone.devices);
            micDropdown.ClearOptions();
            micDropdown.AddOptions(micOptions);
        }

        void Update()
        {
        
        }
    }
}
