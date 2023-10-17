using System.Collections.Generic;
using UnityEngine;


namespace GlobalMap.Map
{
    [System.Serializable]
    public class MissionData
    {
        public Vector2 Position;
        public string NameMission;
        public float Number;
        [TextArea(3, 10)] public string Description;
        [TextArea(3, 10)] public string ActionText;
        [Space]
        public TypeHeroes[] OpenHeroes;
        [Space]
        public string PlayerText;
        public string EnemyText;
        [Space]
        public float[] PrevMission;
        [Space]
        public float[] NextMission;
        [Space]
        public ScoreHeroes[] ScoreHeroes;
        public float NumberDoubleMission = -1;
        public Vector2 GetScale() => new Vector2(0.4f, 0.4f);


        private PreviousMissions prevMission;

        public void SetupOpenHeroes(List<int> numberOpenHeroes)
        {
            OpenHeroes = new TypeHeroes[numberOpenHeroes.Count];
            for (int i = 0; i < numberOpenHeroes.Count; i++)
            {
                OpenHeroes[i] = (TypeHeroes)numberOpenHeroes[i];
            }
        }
   
        public void SetupPrevMission()
        {
            prevMission = new PreviousMissions();
            foreach (var number in PrevMission)
            {
                prevMission.AddMission(number);
            }
        }
        public void RemovePrevLink(float number)=> prevMission.RemoveMission(number);
        public bool CheckNullPrevMission() => prevMission.CheckNullPrevMission();
        public bool CheckPrevMission(float number) => prevMission.CheckMission(number);

        public bool CheckPrevMission() => prevMission.CheckIsOpenPrevMissions();

        public void OpenMission(float number) => prevMission.OpenMission(number);

    }

    [System.Serializable]
    public class ScoreHeroes
    {
        public int Score;
        public TypeHeroes Hero;
    }

    public class PreviousMissions
    {
        private Dictionary<float, bool> prevMissions;

        public PreviousMissions()
        {
            prevMissions = new Dictionary<float, bool>();
        }


        public void OpenMission(float number)
        {
            if (prevMissions.TryGetValue(number, out bool _))
            {
                prevMissions[number] = true;
            }
        }

        public bool CheckMission(float number)
        {
            if (prevMissions.TryGetValue(number, out bool _))
                return true;
            return false;
        }

        public bool CheckNullPrevMission()
        {
            if (prevMissions.Count == 0)
                return true;
            return false;
        }

        public bool CheckIsOpenPrevMissions()
        {
            if (prevMissions.Count == 0)
            {
                return false;
            }
            foreach (var keyValue in prevMissions)
            {
                if (keyValue.Value == false)
                {
                    return false;
                }
            }
            return true;
        }


        public void AddMission(float number) => prevMissions.Add(number, false);
        public void RemoveMission(float number)=> prevMissions.Remove(number);
       
    }
}
