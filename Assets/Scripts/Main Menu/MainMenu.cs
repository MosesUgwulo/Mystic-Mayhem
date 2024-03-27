using System;
using Audio;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_Menu
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.instance.PlayAmbient("MainMenu");
        }

        public void PlayGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
