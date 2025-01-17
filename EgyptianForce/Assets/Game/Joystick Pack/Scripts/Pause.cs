using UnityEngine;

namespace Game
{
    public class Pause : MonoBehaviour
    {
        [SerializeField] private GameObject _pausePanel;
        
        public void PauseGame()
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }

        public void UnPauseGame()
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}