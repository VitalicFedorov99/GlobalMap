using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalTimeDisactiveMission
    {

        public MissionBuilder CurrentMission;
        public SignalTimeDisactiveMission (MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }
}
