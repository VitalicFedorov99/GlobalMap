using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Map
{
    [CreateAssetMenu(fileName = "mapConfig", menuName = "ScriptableObjects/MapConfig")]
    public class SOMapConfig : ScriptableObject
    {
        [SerializeField] private List<MissionData> missions;
        public List<MissionData> GetMissions() => missions;

    }
}
