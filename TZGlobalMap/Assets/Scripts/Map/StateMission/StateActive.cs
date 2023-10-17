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
        }

        public void Exit()
        {
            mission.GetColorizeComponent().PaintStartColor();
        }

        public void PointerClick()
        {
            bus.Invoke(new SignalOpenMission(mission));
            bus.Invoke(new SignalStateTimeDeactivateMission(mission));
        }

        public void PointerEnter()
        {
            mission.GetColorizeComponent().OnPaint(Color.green);
        }

        public void PointerExit()
        {
            mission.GetColorizeComponent().PaintStartColor();
        }
    }
}
