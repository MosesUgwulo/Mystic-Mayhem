// using System;
// using Audio;
// using UnityEngine;
//
// namespace Player
// {
//     public class HandleAudio : MonoBehaviour
//     {
//         private string _currentBiome = "";
//         private string _previousBiome = "";
//         private void OnTriggerEnter(Collider other)
//         {
//             string biomeName = other.tag;
//             
//             if (biomeName == _currentBiome) return;
//
//             if (!string.IsNullOrEmpty(_currentBiome))
//             {
//                 AudioManager.instance.Pause(_currentBiome);
//             }
//             
//             _previousBiome = _currentBiome;
//             _currentBiome = biomeName;
//
//             if (biomeName == _previousBiome)
//             {
//                 AudioManager.instance.Unpause(biomeName);
//             }
//             else
//             {
//                 switch (other.tag)
//                 {
//                     case "Village":
//                         AudioManager.instance.Play("Village");
//                         break;
//                     case "Fire":
//                         AudioManager.instance.Play("Fire");
//                         break;
//                     case "Water":
//                         AudioManager.instance.Play("Water");
//                         break;
//                 }
//             }
//             
//         }
//     }
// }
