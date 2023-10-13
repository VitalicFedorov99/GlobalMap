using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalActiveMission 
    {
        public MissionBuilder CurrentMission;

        public SignalActiveMission(MissionBuilder mission)
        {
            CurrentMission = mission;
        }

    }
}
