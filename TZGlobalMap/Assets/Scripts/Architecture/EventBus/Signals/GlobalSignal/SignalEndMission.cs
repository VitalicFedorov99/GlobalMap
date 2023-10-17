using GlobalMap.Map;
using GlobalMap.Heroes;

namespace GlobalMap.Signals
{
    public class SignalEndMission
    {
        public SOHero CurrenHero;
        public MissionBuilder CurrentMission;

        public SignalEndMission(MissionBuilder mission, SOHero hero) 
        {
            CurrentMission = mission;
            CurrenHero = hero;
        }
    }
}