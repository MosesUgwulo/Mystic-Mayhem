using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using HuggingFace.API;
using Spells;

public class SpeechRecognitionAPI : MonoBehaviour
{
    
    private AudioClip _audioClip;
    private byte[] _bytes;
    private bool _isRecording;
    
    private List<MagicSystem> _spells;
    
    private void Awake()
    {
        // Get the MagicSystem game object from the parent of this game object.
        GameObject magicSystem = GameObject.Find("MagicSystem");
        
        // Get all the Spell components in MagicSystem game object.
        var spellsArray = magicSystem.GetComponents<MagicSystem>();
        _spells = spellsArray.ToList();
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !_isRecording)
        {
            StartRecording();
        }
        
        if (Input.GetMouseButtonUp(1) && _isRecording)
        {
            StopRecording();
        }
    }


    private void StartRecording()
    {
        Debug.Log("Recording started");
        _audioClip = Microphone.Start(null, true, 10, 44100);
        _isRecording = true;
    }
    
    private void StopRecording()
    {
        Debug.Log("Recording stopped");
        var position = Microphone.GetPosition(null);
        Microphone.End(null);
        var samples = new float[position * _audioClip.channels];
        _audioClip.GetData(samples, 0);
        _bytes = EncodeAsWav(samples, _audioClip.frequency, _audioClip.channels);
        _isRecording = false;
        UploadRecording();
    }

    private void UploadRecording()
    {
        HuggingFaceAPI.AutomaticSpeechRecognition(_bytes, response =>
        {
            string cleanedResponse = response.StripPunctuation().ToLower();
            Debug.Log("'" + cleanedResponse + "'");
            
            // var spell = _spells.FirstOrDefault(s => s.phrases.Any(p => p.ToLower() == cleanedResponse) && !s.isEnemySpell);
            var spell = _spells.FirstOrDefault(s => s.phrases.Any(p => cleanedResponse.Contains(p.ToLower())) && !s.isEnemySpell);
            
            if (spell == null)
                return;
            spell.CastSpell();
        }, error =>
        {
            Debug.LogError(error);
        });
    }

    private byte[] EncodeAsWav(float[] samples, int audioClipFrequency, int audioClipChannels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort) 1);
                writer.Write((ushort) audioClipChannels);
                writer.Write(audioClipFrequency);
                writer.Write(audioClipFrequency * audioClipChannels * 2);
                writer.Write((ushort) (audioClipChannels * 2));
                writer.Write((ushort) 16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);
                
                foreach (var sample in samples)
                {
                    writer.Write((short) (sample * short.MaxValue));
                }
            }
            
            return memoryStream.ToArray();
        }
    }
    
}

public static class StringExtension
{
    public static string StripPunctuation(this string s)
    {
        var sb = new StringBuilder();
        foreach (char c in s)
        {
            if (!char.IsPunctuation(c))
                sb.Append(c);
        }
        return sb.ToString().Trim();
    }
}
