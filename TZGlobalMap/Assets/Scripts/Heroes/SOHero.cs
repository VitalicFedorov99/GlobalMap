using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Heroes
{
    [CreateAssetMenu(fileName = "Hero", menuName = "ScriptableObjects/HeroSO")]
    public class SOHero : ScriptableObject
    {
        public Sprite Icon;
        public string Name;
        public int Score;
        public TypeHeroes HeroType;

        public void ClearScore() => Score = 0;
        
        
    }
}
public enum TypeHeroes
{
    NoOpen,
    Hawk,
    Crow,
    Main,
    hero1,
    hero2,
    All
}
