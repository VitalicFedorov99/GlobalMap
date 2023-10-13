using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

using GlobalMap.Architecture;
using GlobalMap.Signals;


namespace GlobalMap.Heroes
{
    [System.Serializable]
    public class CollectionHeroes : IService
    {
        [SerializeField] private List<SOHero> soHeroes;

        private Dictionary<TypeHeroes, SOHero> openHeroes;

        private EventBus eventBus;
        private TypeHeroes currentType;
        public SOHero GetSOHeroWithType(TypeHeroes typeHero) => soHeroes.Where(t => t.HeroType == typeHero).SingleOrDefault();


        public void Setup(EventBus bus)
        {
            eventBus = bus;
            openHeroes = new Dictionary<TypeHeroes, SOHero>();
            RegisterEvent();
        }

        public void AddHeroes(SOHero hero)
        {
            openHeroes.TryAdd(hero.HeroType, hero);
        }

        private void RegisterEvent()
        {
            eventBus.Subscribe<SignalEndMission>(SetupOpenHeroType);
            eventBus.Subscribe<SignalCheckOpenNewHero>(CreateHero);
        }


        private void SetupOpenHeroType(SignalEndMission signal)
        {
            var heroes = signal.CurrentMission.GetMissionData().OpenHeroes;


            foreach (var hero in heroes)
            {
                if (!openHeroes.TryGetValue(hero, out SOHero _))
                {
                    currentType = hero;
                }
            }
        }
        private void CreateHero(SignalCheckOpenNewHero signal)
        {
           eventBus.Invoke(new SignalCreateHero(GetSOHeroWithType(currentType)));
        }



    }
}
