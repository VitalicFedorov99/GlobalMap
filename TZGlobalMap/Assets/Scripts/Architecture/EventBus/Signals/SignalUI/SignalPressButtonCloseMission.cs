using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalPressButtonCloseMission 
    {
        public MissionBuilder CurrentMission;

        public SignalPressButtonCloseMission(MissionBuilder mission) 
        {
            CurrentMission = mission;
        }

    }
}
