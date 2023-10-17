using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalOpenMission
    {
        public MissionBuilder CurrentMission;

        public SignalOpenMission(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }
}
