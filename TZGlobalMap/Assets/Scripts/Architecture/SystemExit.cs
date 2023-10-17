using UnityEngine;
using UnityEngine.UI;


namespace GlobalMap.Architecture
{
    [System.Serializable]
    public class SystemExit
    {
        [SerializeField] private Button buttonExit;

        public void Setup() 
        {
            buttonExit.onClick.RemoveAllListeners();
            buttonExit.onClick.AddListener(PressButtonExit);
        }

        private void PressButtonExit() => Application.Quit();
    }
}
