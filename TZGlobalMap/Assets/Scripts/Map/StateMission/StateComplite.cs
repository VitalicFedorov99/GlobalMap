using GlobalMap.Architecture;
using GlobalMap.Signals;
using UnityEngine;
namespace GlobalMap.Map {
    public class StateComplite : IStateMission
    {
        private MissionBuilder mission;
        private EventBus bus;
        public StateComplite(MissionBuilder mis, EventBus b)
        {
            mission = mis;
            bus = b;
        }

        public void Enter()
        {
            mission.GetColorizeComponent().OnPaint(Color.yellow);
        }

        public void Exit()
        {

        }

        public void PointerClick()
        {
           
        }

        public void PointerEnter()
        {
            
        }

        public void PointerExit()
        {
            
        }
    }
}
