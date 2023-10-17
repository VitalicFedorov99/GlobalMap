using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalStateCompliteMission
    {
        public MissionBuilder CurrentMission;
        
        public SignalStateCompliteMission(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }
}
