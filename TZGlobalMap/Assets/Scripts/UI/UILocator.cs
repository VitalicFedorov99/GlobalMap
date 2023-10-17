using System.Collections.Generic;
using UnityEngine;

using GlobalMap.UI;

namespace GlobalMap.Architecture
{
    public class UILocator : MonoBehaviour
    {
        [SerializeField] private MissionView missionView;
        [SerializeField] private CompliteMissionView compliteMissionView;
        [SerializeField] private SystemRestart systemRestart;
        [SerializeField] private SystemExit systemExit;

        private Dictionary<TypeHeroes, HeroView> heroesView;
        public void Setup(EventBus bus, CollectionImageMission collection)
        {
            missionView.Setup(bus, collection);
            compliteMissionView.Setup(bus, collection);
            systemRestart.Setup();
            systemExit.Setup();
            heroesView = new Dictionary<TypeHeroes, HeroView>();
        }

        public void AddHeroView(HeroView hero)
        {
            if (!heroesView.TryAdd(hero.GetTypeHero(), hero))
            {
                Destroy(hero.gameObject);
            }
        }
    }
}
