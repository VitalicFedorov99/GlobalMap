using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

namespace GlobalMap.Architecture
{
    [System.Serializable]
    public class SystemRestart
    {
        [SerializeField] private Button restartButton;
        public void Setup() 
        {
            
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(PressButtonRestart);
        }

        private void PressButtonRestart() 
        {
            SceneManager.LoadScene("Game");
        }

    }
}
