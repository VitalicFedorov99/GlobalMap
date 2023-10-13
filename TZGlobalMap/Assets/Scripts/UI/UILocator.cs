using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.UI;

namespace GlobalMap.Architecture
{


    public class UILocator : MonoBehaviour, IService
    {
        [SerializeField] private MissionView missionView;
        [SerializeField] private CompliteMissionView compliteMissionView;
        
        private Dictionary<TypeHeroes,HeroView> heroesView;
        public void Setup(EventBus bus)
        {
            missionView.Setup(bus);
            compliteMissionView.Setup(bus);
            heroesView = new Dictionary<TypeHeroes,HeroView>();
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
