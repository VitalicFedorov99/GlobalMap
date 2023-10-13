using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;

namespace GlobalMap.Architecture
{
    [System.Serializable]
    public class SOFactoried: IFactoried
    {
        public SOMapConfig soMapConfig;
        public List<MissionData> GetMissionDatas() => soMapConfig.GetMissions();



    }
}
