using UnityEngine;

namespace GlobalMap.Architecture
{
    public static class JsonParser
    {

        public static void ParsingMissionData(out ListMissionDataJson listMissions, string nameDoc)
        {
            string filePath = Application.streamingAssetsPath + "/" + nameDoc + ".json";// "Map1.json как пример"
            string jsonText = System.IO.File.ReadAllText(filePath);
            listMissions = JsonUtility.FromJson<ListMissionDataJson>(jsonText);
        }
    }
}
