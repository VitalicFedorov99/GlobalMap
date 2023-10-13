using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

using GlobalMap.Architecture;
using GlobalMap.Signals;

namespace GlobalMap.UI
{
    [System.Serializable]
    public class CompliteMissionView
    {
        [SerializeField] private Button buttonEndMission;
        [SerializeField] private TMP_Text nameMission;
        [SerializeField] private Image imageMission;
        [SerializeField] private RectTransform imageObject;

        private EventBus eventBus;

        public void Setup(EventBus bus)
        {
            eventBus = bus;
            buttonEndMission.onClick.RemoveAllListeners();
            buttonEndMission.onClick.AddListener(EndMission);
            RegisterEvent();
        }

        private void RegisterEvent() 
        {
            eventBus.Subscribe<SignalEndMission>(OpenDisplay);
        }
            
        private void EndMission() 
        {
            imageObject.gameObject.SetActive(false);
            eventBus.Invoke(new SignalPressButtonCompliteMission());
            eventBus.Invoke(new SignalCheckOpenNewHero());
        }


        

        private void OpenDisplay(SignalEndMission signal)
        {
            imageObject.gameObject.SetActive(true);
        }
      



    }
}
