using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Architecture;
using GlobalMap.Signals;

namespace GlobalMap.Map
{
    public class StateTimeDisactive : IStateMission
    {
        private MissionBuilder mission;
        private EventBus bus;
        public StateTimeDisactive(MissionBuilder mis, EventBus b)
        {
            mission = mis;
            bus = b;
        }
        public void Enter()
        {
            mission.GetColorizeComponent().OnPaint(Color.black);
        }

        public void Exit()
        {
            mission.GetColorizeComponent().PaintStartColor();
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
