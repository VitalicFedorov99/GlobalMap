using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalStateBlockMission
    {
        public MissionBuilder CurrentMission;

        public SignalStateBlockMission(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }

    }
}
