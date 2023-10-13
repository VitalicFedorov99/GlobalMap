using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Architecture;
using GlobalMap.Signals;

namespace GlobalMap.Map
{
    public class StateActive : IStateMission
    {

        private MissionBuilder mission;
        private EventBus bus;

        public StateActive(MissionBuilder mis, EventBus b)
        {
            mission = mis;
            bus = b;
        }
        public void Enter()
        {
           // bus.Invoke(new SignalActiveMission(mission));
        }

        public void Exit()
        {
            mission.GetColorizeComponent().PaintStartColor();
        }

        public void PointerClick()
        {
            bus.Invoke(new SignalOpenMission(mission));
            bus.Invoke(new SignalTimeDisactiveMission(mission));
        }

        public void PointerEnter()
        {
            mission.GetColorizeComponent().OnHover();
        }

        public void PointerExit()
        {
            mission.GetColorizeComponent().PaintStartColor();
        }
    }
}
