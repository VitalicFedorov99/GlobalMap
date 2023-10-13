using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalPressButtonStart
    {
        public MissionBuilder CurrentMission;

        public SignalPressButtonStart(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }
    }
}
