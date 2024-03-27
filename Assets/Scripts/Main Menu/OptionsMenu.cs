using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Main_Menu
{
    public class OptionsMenu : MonoBehaviour
    {
        public AudioMixer audioMixer;
        public Slider volumeSlider;
        public Slider sensitivitySlider;

        void Start()
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume", 0.5f);
            SetVolume(savedVolume);
            volumeSlider.value = savedVolume;
            float savedSensitivity = PlayerPrefs.GetFloat("SensitivityX", 300f);
            SetSensitivity(savedSensitivity);
            sensitivitySlider.value = savedSensitivity;
        }
        
        public void SetSensitivity(float sensitivity)
        {
            PlayerPrefs.SetFloat("SensitivityX", sensitivity);
            PlayerPrefs.SetFloat("SensitivityY", sensitivity);
            PlayerPrefs.Save();
        }
        
        public void SetVolume(float volume)
        {
            audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("Volume", volume);
            PlayerPrefs.Save();
        }
        
        void Update()
        {
        
        }
    }
}
