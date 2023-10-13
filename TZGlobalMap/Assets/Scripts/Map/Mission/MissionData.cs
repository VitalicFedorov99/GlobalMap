using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GlobalMap.Map
{
    [System.Serializable]
    public class MissionData
    {
        public Vector2 Position;
        public Vector2 Scale;
        public string NameMission;
        public float Number;
        [TextArea(3, 10)] public string Description;
        [TextArea(3, 10)] public string ActionText;
        // public TypeHeroes[] TypePlayerHeroes;
        public TypeHeroes[] TypeEnemyHeroes;
        public TypeHeroes[] OpenHeroes;
        public float[] PrevMission;
        public float[] DisactiveMissions;
        public float[] nextMission;
        public ScoreHeroes[] ScoreHeroes;
        public float NumberDoubleMission = -1;





        private PreviousMissions prevMission;



        public void RemovePrevLink(float number)
        {
            prevMission.RemoveMission(number);
            Debug.LogError("У " + Number + "Удалил предшественника " + number);
        }


        public void SetupPrevMission()
        {
            prevMission = new PreviousMissions();
            foreach (var number in PrevMission)
            {
                prevMission.AddMission(number);
            }
        }



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
                Debug.LogError(prevMissions[number]);
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
            foreach (var keyValue in prevMissions)
            {
                if (keyValue.Value == false)
                {
                    return false;
                }
            }
            return true;
        }


        public void AddMission(float number)
        {
            prevMissions.Add(number, false);
        }

        public void RemoveMission(float number)
        {
            prevMissions.Remove(number);
        }
    }
}
