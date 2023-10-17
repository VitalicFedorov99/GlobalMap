using GlobalMap.Map;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Architecture
{
    [System.Serializable]
    public class MissionDataJson
    {
        public float Number;
        public Position Position;
        public string NameMission;
        public string Description;
        public string ActionText;
        public string PlayerHeroes;
        public string EnemyHeroes;
        public List<float> PrevMissions;
        public List<float> NextMissions;
        public List<int> OpenHeroes;
        public List<ScoreHeroes> ScoreHeroes;
        public float NumberDoubleMission;
        public MissionDataJson()
        {
            Position = new Position();
            PrevMissions = new List<float>();
            OpenHeroes = new List<int>();
            ScoreHeroes = new List<ScoreHeroes>();
        }

    }
    [System.Serializable]
    public class Position
    {
        public float X;
        public float Y;
    }
    [System.Serializable]
    public class ListMissionDataJson : IFactoried
    {

        [SerializeField] private List<MissionDataJson> missionDatasJson;
        [SerializeField] private List<MissionData> missionDatas;
        public ListMissionDataJson()
        {
            missionDatasJson = new List<MissionDataJson>();
            missionDatas = new List<MissionData>();
        }


        public void ConvertJsonDataInMissionData()
        {
            foreach (var dataJson in missionDatasJson)
            {
                MissionData missionData = new MissionData
                {
                    ActionText = dataJson.ActionText,
                    Description = dataJson.Description,
                    EnemyText = dataJson.EnemyHeroes,
                    NameMission = dataJson.NameMission,
                    NextMission = dataJson.NextMissions.ToArray(),
                    Number = dataJson.Number,
                    NumberDoubleMission = dataJson.NumberDoubleMission
                };
                missionData.SetupOpenHeroes(dataJson.OpenHeroes);
                missionData.PlayerText = dataJson.PlayerHeroes;
                missionData.Position = new Vector2(dataJson.Position.X, dataJson.Position.Y);
                missionData.PrevMission = dataJson.PrevMissions.ToArray();
                missionData.SetupPrevMission();
                missionData.ScoreHeroes = dataJson.ScoreHeroes.ToArray();
                missionDatas.Add(missionData);
            }
        }
        public List<MissionData> GetMissionDatas() => missionDatas;


    }
}
