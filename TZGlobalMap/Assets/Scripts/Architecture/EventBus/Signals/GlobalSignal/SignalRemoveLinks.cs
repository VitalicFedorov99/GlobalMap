using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalRemoveLinks
    {
        public MissionBuilder CurrentMission;

        public SignalRemoveLinks(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }
}

