using UnityEngine;

using GlobalMap.Map;
using GlobalMap.Heroes;
using GlobalMap.Signals;

namespace GlobalMap.Architecture
{
    public class ServiceLocatorLoader_Game : MonoBehaviour
    {

        [SerializeField] private Factory factory;
        [SerializeField] private SOFactoried soFactoried;
        [SerializeField] private UILocator uiLocator;
        [SerializeField] private CollectionHeroes collectionHeroes;
        [SerializeField] private CollectionImageMission collectionImage;
        [SerializeField] private string nameJsonFile;

        [SerializeField] private TypeReadMissionData typeRead;



        private ListMissionDataJson listMissionDataJson;

        private IFactoried currentFactoried;
        private MediatorMission mediator;
        private EventBus eventBus;
        private GameMap gameMap;
        private void Awake()
        {
            Setup();
        }

        private void Start()
        {
            StartGame();
        }
        private void Setup()
        {
            eventBus = new EventBus();
            ReadMissionDataJson(nameJsonFile);
            gameMap = new GameMap(eventBus);
            mediator = new MediatorMission(eventBus);
            collectionHeroes.Setup(eventBus);

            switch (typeRead)
            {
                case TypeReadMissionData.ReadSO:
                    currentFactoried = soFactoried;
                    break;
                case TypeReadMissionData.ReadJson:
                    currentFactoried = listMissionDataJson;
                    break;
            }


            factory.Setup(currentFactoried, eventBus, uiLocator, collectionHeroes);
            factory.CreateMissions(gameMap);
            uiLocator.Setup(eventBus, collectionImage);
            gameMap.SetupMap();
        }

        private void ReadMissionDataJson(string nameFile)
        {
            listMissionDataJson = new ListMissionDataJson();
            JsonParser.ParsingMissionData(out listMissionDataJson, nameFile);
            listMissionDataJson.ConvertJsonDataInMissionData();
        }

        private void StartGame()
        {
            eventBus.Invoke(new SignalCreateHero(collectionHeroes.GetSOHeroWithType(TypeHeroes.Hawk)));
        }

    }

    public enum TypeReadMissionData
    {
        ReadSO,
        ReadJson
    }
}
