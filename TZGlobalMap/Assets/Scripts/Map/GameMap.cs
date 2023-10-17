using System.Collections.Generic;

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
                    TimeDisactiveMission(keyValue.Key);
                }
            }
        }


        private void RegisterEvent()
        {
            eventBus.Subscribe<SignalEndMission>(CompliteMission,3);
            eventBus.Subscribe<SignalPressButtonCompliteMission>(StartNewStep);
        }

        private void TimeDisactiveMission(float number)
        {
            if (missions.TryGetValue(number, out MissionBuilder mission))
            {
                eventBus.Invoke(new SignalStateTimeDeactivateMission(mission));
            }
        }

        private void CompliteMission(SignalEndMission signal)
        {
            eventBus.Invoke(new SignalStateCompliteMission(signal.CurrentMission));
            float numberDoubleMission = signal.CurrentMission.GetMissionData().NumberDoubleMission;

            if(missions.TryGetValue(numberDoubleMission, out MissionBuilder mission)) 
            {
                eventBus.Invoke(new SignalStateBlockMission(mission));
            }

            OpenMission(signal.CurrentMission.GetMissionData());
        }

        private void OpenMission(MissionData data)
        {
            foreach (var number in data.NextMission)
            {
                if (missions.TryGetValue(number, out MissionBuilder mission))
                {
                    mission.GetMissionData().OpenMission(data.Number); 
                    if (mission.GetMissionData().CheckPrevMission())
                    {
                        eventBus.Invoke(new SignalStateActivateMission(mission));
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
