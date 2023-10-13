using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Architecture;
using GlobalMap.Signals;

namespace GlobalMap.Map
{
    public class GameMap
    {

        private Dictionary<float, MissionBuilder> missions;
        private EventBus eventBus;
        public GameMap(EventBus bus)
        {
            missions = new Dictionary<float, MissionBuilder>();
            eventBus = bus;
            RegisterEvent();
        }


        private void RegisterEvent()
        {
            eventBus.Subscribe<SignalEndMission>(CompliteMission,3);
            eventBus.Subscribe<SignalPressButtonCompliteMission>(StartNewStep);
        }


        public void AddMission(MissionBuilder mission)
        {
            missions.TryAdd(mission.GetMissionData().Number, mission);
        }




        public void SetupMap()
        {
            foreach (var keyValue in missions)
            {
                if (keyValue.Value.GetMissionData().PrevMission.Length > 0)
                {
                    //
                    TimeDisactiveMission(keyValue.Key);
                    // BlockMission(keyValue.Key);
                }
            }
        }

        private void TimeDisactiveMission(float number)
        {
            if (missions.TryGetValue(number, out MissionBuilder mission))
            {
                eventBus.Invoke(new SignalTimeDisactiveMission(mission));
            }
        }
        private void CompliteMission(SignalEndMission signal)
        {


            eventBus.Invoke(new SignalCompliteMission(signal.CurrentMission));
            float numberDoubleMission = signal.CurrentMission.GetMissionData().NumberDoubleMission;
            if(missions.TryGetValue(numberDoubleMission, out MissionBuilder mission)) 
            {
                eventBus.Invoke(new SignalBlockMission(mission));
            }
            



            OpenMission(signal.CurrentMission.GetMissionData());
        }

        private void OpenMission(MissionData data)
        {
            foreach (var number in data.nextMission)
            {
                if (missions.TryGetValue(number, out MissionBuilder mission))
                {
                    mission.GetMissionData().OpenMission(data.Number); 
                    if (mission.GetMissionData().CheckPrevMission())
                    {
                        eventBus.Invoke(new SignalActiveMission(mission));
                    }
                }
            }
        }

        private void StartNewStep(SignalPressButtonCompliteMission signal)
        {

            eventBus.Invoke(new SignalNewStep());
        }


    }
}
