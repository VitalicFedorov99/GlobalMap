using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private MediatorMission mediator;


        private EventBus eventBus;
        private GameMap gameMap;
        private void Awake()
        {
            Setup();
            RegisterService();

        }

        private void Start()
        {
            StartGame();
        }
        private void Setup()
        {
            eventBus = new EventBus();
            gameMap = new GameMap(eventBus);
            mediator = new MediatorMission(eventBus);
            collectionHeroes.Setup(eventBus);
            factory.Setup(soFactoried, eventBus, uiLocator, collectionHeroes);
            factory.CreateMissions(gameMap, eventBus);
            uiLocator.Setup(eventBus);



            gameMap.SetupMap();
            /*  eventBus = new CustomEventBus();
              queueStepCharacters = new QueueStepCharacters(eventBus);
              uimanager.Setup(eventBus);
              stateGameController = new StateGameController(eventBus);
              //stateGameController.Initialize();
              factory.Setup(characters, fightConfig);
              player = new Player(1);
              playerEnemy = new Player(2);*/

        }

        private void RegisterService()
        {
            ServiceLocator.Initialize();
            ServiceLocator.Current.Register(factory);
            ServiceLocator.Current.Register(eventBus);
            ServiceLocator.Current.Register(collectionHeroes);
            ServiceLocator.Current.Register(mediator);
            /*  ServiceLocator.Current.Register(eventBus);
              ServiceLocator.Current.Register(queueStepCharacters);*/
        }




        private void StartGame()
        {
            eventBus.Invoke(new SignalCreateHero(collectionHeroes.GetSOHeroWithType(TypeHeroes.Hawk)));
            eventBus.Invoke(new SignalCreateHero(collectionHeroes.GetSOHeroWithType(TypeHeroes.hero1)));
            //eventBus.Invoke(new SignalCreateHero(collectionHeroes.GetSOHeroWithType(TypeHeroes.hero2)));
            //eventBus.Invoke(new SignalCreateHero(collectionHeroes.GetSOHeroWithType(TypeHeroes.Crow)));
        }

    }
}
