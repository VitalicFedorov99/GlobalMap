using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Signals
{
    public class SignalClickMission
    {
        public MissionBuilder CurrentMission;

        public SignalClickMission(MissionBuilder mission)
        {
            Debug.LogError("������ ������ " + mission.name);
            CurrentMission = mission;
        }
    }
}
