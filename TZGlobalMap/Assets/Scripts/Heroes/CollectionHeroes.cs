using System.Collections.Generic;
using UnityEngine;
using System.Linq;


using GlobalMap.Architecture;
using GlobalMap.Signals;


namespace GlobalMap.Heroes
{
    [System.Serializable]
    public class CollectionHeroes
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
            foreach (var so in soHeroes)
            {
                so.ClearScore();
            }
            RegisterEvent();
        }

        public void AddHeroes(SOHero hero)
        {
            openHeroes.TryAdd(hero.HeroType, hero);
        }
        private void RegisterEvent()
        {
            eventBus.Subscribe<SignalEndMission>(SetupOpenHeroType);
            eventBus.Subscribe<SignalEndMission>(UpdateScoreHeroes, 1);
            eventBus.Subscribe<SignalCheckOpenNewHero>(CreateHero);
        }

        private void SetupOpenHeroType(SignalEndMission signal)
        {
            var heroes = signal.CurrentMission.GetMissionData().OpenHeroes;

            if (heroes == null)
            {
                currentType = TypeHeroes.NoOpen;
                return;
            }

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
            if (currentType == TypeHeroes.NoOpen)
                return;
            if (openHeroes.TryGetValue(currentType, out SOHero _))
                return;

            eventBus.Invoke(new SignalCreateHero(GetSOHeroWithType(currentType)));
        }

        private void UpdateScoreHeroes(SignalEndMission signal)
        {
            var scoreHeroes = signal.CurrentMission.GetMissionData().ScoreHeroes;
            foreach (var sh in scoreHeroes)
            {
                switch (sh.Hero)
                {
                    case TypeHeroes.Main:
                        signal.CurrenHero.Score += sh.Score;
                        break;
                    default:
                        if (sh.Score < 0)
                        {
                            var currentHero = GetSOHeroWithType(sh.Hero);
                            if (currentHero != null)
                                currentHero.Score += sh.Score;
                        }
                        else
                        {
                            if (openHeroes.TryGetValue(sh.Hero, out SOHero hero))
                            {
                                hero.Score += sh.Score;
                            }
                        }

                        break;
                }
            }
        }




    }
}
