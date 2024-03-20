using System;
using System.Collections;
using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        public Sound[] sounds;
        private string _currentBiome = "";
        private float _pauseDelay = 0.5f;
        
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
            
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.loop = sound.loop;
                sound.source.playOnAwake = sound.playOnAwake;
            }
        }

        public void PlayAmbient(string name)
        {
            if (name != _currentBiome)
            {
                if (!string.IsNullOrEmpty(_currentBiome))
                {
                    Pause(_currentBiome);
                }
            }
            
            var sound = Array.Find(sounds, s => s.name == name);
            
            if (sound == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            
            if (!sound.source.isPlaying)
            {
                sound.source.Play();
            }
            
            _currentBiome = name;
        }

        public void PlayFX(string name)
        {
            var sound = Array.Find(sounds, s => s.name == name);
            
            if (sound == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }
            
            sound.source.Play();
        }
        
        public void TryPause(string name)
        {
            StartCoroutine(DelayedPause(name));
        }

        public void Pause(string name)
        {
            var sound = Array.Find(sounds, s => s.name == name);
            
            if (sound != null && sound.source.isPlaying)
            {
                sound.source.Pause();
            }
        }
        
        public IEnumerator DelayedPause(string name)
        {
            yield return new WaitForSeconds(_pauseDelay);
            Pause(name);
        }

        public void Unpause(string name)
        {
            var sound = Array.Find(sounds, s => s.name == name);
            
            if (sound != null)
            {
                sound.source.UnPause();
            }
        }

        public void Stop(string name)
        {
            var sound = Array.Find(sounds, s => s.name == name);
            
            if (sound != null)
            {
                sound.source.Stop();
            }
        
        }
        
        public bool IsPlaying(string name)
        {
            var sound = Array.Find(sounds, s => s.name == name);
            return sound != null && sound.source.isPlaying;
        }
    }
}
