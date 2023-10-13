using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Architecture;
using GlobalMap.Signals;

namespace GlobalMap.Map
{
    public class StateBlock : IStateMission
    {
        private MissionBuilder mission;
        private EventBus bus;

        public StateBlock(MissionBuilder mis, EventBus b)
        {
            mission = mis;
            bus = b;
        }
        public void Enter()
        {
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