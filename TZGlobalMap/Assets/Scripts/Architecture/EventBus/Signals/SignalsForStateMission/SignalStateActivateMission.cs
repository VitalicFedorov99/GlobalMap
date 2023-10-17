using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalStateActivateMission
    {
        public MissionBuilder CurrentMission;

        public SignalStateActivateMission(MissionBuilder mission)
        {
            CurrentMission = mission;
        }

    }
}
