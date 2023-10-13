using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using GlobalMap.Heroes;
using GlobalMap.Signals;
using GlobalMap.Map;

namespace GlobalMap.Architecture
{

    [System.Serializable]
    public class MediatorMission : IService
    {
        [SerializeField] private SOHero currentHero;
        [SerializeField] private MissionBuilder currentMission;

        private EventBus eventBus;


        public MediatorMission(EventBus bus)
        {
            eventBus = bus; 
            RegisterEvent();
        }


        private void RegisterEvent()
        {
            eventBus.Subscribe<SignalPressButtonChooseHero>(SetupHero);
            eventBus.Subscribe<SignalOpenMission>(SetupMission);
            eventBus.Subscribe<SignalNewStep>(ResetData);
            eventBus.Subscribe<SignalPressButtonCloseMission>(ResetMission);
            eventBus.Subscribe<SignalStartMission>(StartMission);

        }

        private void SetupHero(SignalPressButtonChooseHero signal)
        {
            currentHero = signal.CurrentSOHero;
            eventBus.Invoke(new SignalRemoveBlockButtonStartMission());
        }

        private void SetupMission(SignalOpenMission signal)
        {
            currentMission = signal.CurrentMission;
        }

        private void ResetMission(SignalPressButtonCloseMission signal) 
        {
            currentMission = null; 
        }

        private void StartMission(SignalStartMission signal) 
        {
            eventBus.Invoke(new SignalEndMission(currentMission, currentHero));
        }

        private void ResetData(SignalNewStep signal) 
        {
            currentHero = null;
            currentMission = null;
        }
    }
}
