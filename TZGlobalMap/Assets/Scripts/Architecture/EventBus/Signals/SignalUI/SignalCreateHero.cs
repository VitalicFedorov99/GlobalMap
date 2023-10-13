using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using GlobalMap.Heroes;

namespace GlobalMap.Signals
{
    public class SignalCreateHero
    {
        public SOHero CurrentSOHero;

        public SignalCreateHero(SOHero soHero) 
        {
            CurrentSOHero = soHero;
        }
    }
}
