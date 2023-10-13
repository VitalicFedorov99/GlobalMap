using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalBlockMission
    {
        public MissionBuilder CurrentMission;

        public SignalBlockMission(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }

    }
}
