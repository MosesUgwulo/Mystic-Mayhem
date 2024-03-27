using UnityEngine;

namespace Main_Menu
{
    public class PauseMenu : MonoBehaviour
    {
        private Canvas _pauseMenuUI;
        void Start()
        {
            _pauseMenuUI = GameObject.Find("PauseHUD").GetComponent<Canvas>();
            _pauseMenuUI.enabled = false;
            _pauseMenuUI.sortingOrder = 100;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_pauseMenuUI.enabled)
                {
                    Resume();
                    Debug.Log("Resumed");
                }
                else 
                {
                    Pause();
                    Debug.Log("Paused");
                }
            }
        }

        private void Resume()
        {
            _pauseMenuUI.enabled = false;
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        private void Pause()
        {
            _pauseMenuUI.enabled = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
