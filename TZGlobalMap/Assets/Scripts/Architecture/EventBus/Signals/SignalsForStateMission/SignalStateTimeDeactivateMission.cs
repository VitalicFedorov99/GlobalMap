using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalStateTimeDeactivateMission
    {
        public MissionBuilder CurrentMission;
        public SignalStateTimeDeactivateMission (MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }
}
