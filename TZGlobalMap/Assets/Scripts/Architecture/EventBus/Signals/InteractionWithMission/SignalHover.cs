using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{

    public class SignalHover
    {
        public MissionBuilder CurrentMission;

        public SignalHover(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }

}