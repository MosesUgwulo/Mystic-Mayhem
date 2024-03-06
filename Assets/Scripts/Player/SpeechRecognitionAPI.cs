using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using HuggingFace.API;
using Spells;
using TMPro;
using UnityEngine.UI;

public class SpeechRecognitionAPI : MonoBehaviour
{
    public TMP_Dropdown micDropdown;
    private AudioClip _audioClip;
    private byte[] _bytes;
    private bool _isRecording;
    private string _selectedMic;
    
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
        if (micDropdown.options.Count == 0)
        {
            Debug.LogError("No microphone detected");
            return;
        }
        Debug.Log("Recording started");
        
        _selectedMic = micDropdown.options[micDropdown.value].text;
        int minFreq, maxFreq;
        Microphone.GetDeviceCaps(_selectedMic, out minFreq, out maxFreq);
        
        _audioClip = Microphone.Start(_selectedMic, true, 10, maxFreq > 0 ? maxFreq : 44100);
        _isRecording = true;
    }
    
    private void StopRecording()
    {
        Debug.Log("Recording stopped");
        var position = Microphone.GetPosition(_selectedMic);
        Microphone.End(_selectedMic);
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
            
            LogResponseToFile(cleanedResponse);
            
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

    private void SpeechFolderExists()
    {
        string folderpath = Path.Combine(Directory.GetCurrentDirectory(), "Speech");
        if (!Directory.Exists(folderpath))
        {
            Directory.CreateDirectory(folderpath);
            Debug.Log("Speech folder created at: " + folderpath);
        }
    }

    private void LogResponseToFile(string response)
    {
        SpeechFolderExists();
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Speech");
        string filePath = Path.Combine(folderPath, "response.txt");

        using StreamWriter writer = File.AppendText(filePath);
        writer.WriteLine($"[{DateTime.Now}] {response}");
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
