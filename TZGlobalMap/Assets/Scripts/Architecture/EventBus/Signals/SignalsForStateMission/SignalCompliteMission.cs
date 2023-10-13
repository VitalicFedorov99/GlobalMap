using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalCompliteMission
    {
        public MissionBuilder CurrentMission;
        
        public SignalCompliteMission(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }
}
