using GlobalMap.Heroes;
using GlobalMap.UI;

namespace GlobalMap.Signals
{
    public class SignalPressButtonChooseHero
    {
        public SOHero CurrentSOHero;
        public HeroView CurrentHeroView;
        public SignalPressButtonChooseHero(SOHero hero, HeroView heroView)
        {
            CurrentSOHero = hero;
            CurrentHeroView = heroView;
        }
    }
}