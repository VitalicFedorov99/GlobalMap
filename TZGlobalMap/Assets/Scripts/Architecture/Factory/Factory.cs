using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Map;
using GlobalMap.UI;
using GlobalMap.Signals;
using GlobalMap.Heroes;

namespace GlobalMap.Architecture
{

    public class Factory : MonoBehaviour
    {
        [SerializeField] private MissionBuilder prefabMission;
        [SerializeField] private HeroView prefabHero;
        [SerializeField] private RectTransform parentForHeroView;
        [SerializeField] private TMPro.TMP_Text prefabText;
        [SerializeField] private RectTransform parentCanvas;


        private IFactoried factoried;
        private EventBus eventBus;
        private UILocator uiLocator;
        private CollectionHeroes collectionHeroes;


        public void Setup(IFactoried factor, EventBus bus, UILocator ui, CollectionHeroes collection)
        {
            factoried = factor;
            eventBus = bus;
            uiLocator = ui;
            collectionHeroes = collection;
            RegisterEvents();
        }

        public void CreateMissions(GameMap map)
        {
            var missions = factoried.GetMissionDatas();

            foreach (var mission in missions)
            {
                var tempMission = Instantiate(prefabMission);
                var tempText = Instantiate(prefabText, parentCanvas);
                tempMission.name += " " + mission.Number.ToString();
                PlacementMissions(tempMission, mission);
                tempMission.Setup(mission, tempText, eventBus);
                tempMission.GetMissionData().SetupPrevMission();
                map.AddMission(tempMission);
            }

        }

        private void RegisterEvents()
        {
            eventBus.Subscribe<SignalCreateHero>(SignalCreateHero);
        }

        private void CreateHero(SOHero hero)
        {
            var tempHeroView = Instantiate(prefabHero, parentForHeroView);
            tempHeroView.Setup(eventBus, hero);
            uiLocator.AddHeroView(tempHeroView);
            collectionHeroes.AddHeroes(hero);
        }

        private void SignalCreateHero(SignalCreateHero signal)
        {
            CreateHero(signal.CurrentSOHero);
        }


        private void PlacementMissions(MissionBuilder missionBuilder, MissionData missionData)
        {
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;

            float xPosition = screenWidth * missionData.Position.x;
            float yPosition = screenHeight * missionData.Position.y;


            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(xPosition, yPosition, 0f));
            worldPosition.z = 0;

            Vector3 scale = new Vector3(missionData.GetScale().x, missionData.GetScale().y, 1f);

            missionBuilder.transform.position = worldPosition;
            missionBuilder.transform.localScale = scale;
        }
    }

    public interface IFactoried
    {
        public List<MissionData> GetMissionDatas();

    }
}
