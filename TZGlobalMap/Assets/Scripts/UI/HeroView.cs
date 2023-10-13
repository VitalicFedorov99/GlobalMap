using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

using GlobalMap.Architecture;
using GlobalMap.Heroes;
using GlobalMap.Signals;


namespace GlobalMap.UI
{
    public class HeroView : MonoBehaviour
    {
        [SerializeField] private TMP_Text textScore;
        [SerializeField] private TMP_Text textName;
        [SerializeField] private Image icon;
        [SerializeField] private Button buttonChooseHero;
        [SerializeField] private Image blockImage;

        private Image buttonImage;

        private EventBus eventBus;
        private SOHero soHero;
        public void Setup(EventBus bus, SOHero hero)
        {
            buttonImage = buttonChooseHero.GetComponent<Image>();
            eventBus = bus;
            soHero = hero;
            buttonChooseHero.onClick.RemoveAllListeners();
            buttonChooseHero.onClick.AddListener(PressButtonChooseHero);
            hero.ClearScore();
            UpdateInfo();
            RegisterEvent();
        }


        public TypeHeroes GetTypeHero() => soHero.HeroType;

        private void UpdateInfo()
        {
            textScore.text = soHero.Score.ToString();
            textName.text = soHero.Name;
            icon.sprite = soHero.Icon;

        }



        private void RegisterEvent()
        {
            eventBus.Subscribe<SignalStartMission>(BlockImageOn);
            eventBus.Subscribe<SignalPressButtonCompliteMission>(BlockImageOff);
            eventBus.Subscribe<SignalPressButtonChooseHero>(ColorizeButton);
            eventBus.Subscribe<SignalNewStep>(StartNewStep);
            eventBus.Subscribe<SignalEndMission>(AddScoreHeroes,2);
        }

        private void AddScoreHeroes(SignalEndMission signal)
        {
            var scoreHeroes = signal.CurrentMission.GetMissionData().ScoreHeroes;
            foreach (var sh in scoreHeroes)
            {
                if (sh.Hero == TypeHeroes.Main && signal.CurrenHero == soHero)
                {
                    soHero.Score += sh.Score;
                }
                if (sh.Hero == TypeHeroes.All)
                {
                    soHero.Score += sh.Score;
                }
                if (sh.Hero == soHero.HeroType)
                {
                    soHero.Score += sh.Score;
                }

            }
            UpdateInfo();
        }

        private void BlockImageOn(SignalStartMission signal)
        {
            blockImage.gameObject.SetActive(true);
        }

        private void BlockImageOff(SignalPressButtonCompliteMission signal)
        {
            blockImage.gameObject.SetActive(false);
        }


        private void ColorizeButton(SignalPressButtonChooseHero signal)
        {
            buttonImage.color = Color.white;
            if (signal.CurrentHeroView == this)
            {
                buttonImage.color = Color.green;
            }
        }

        private void StartNewStep(SignalNewStep signal)
        {
            buttonImage.color = Color.white;
            UpdateInfo();
        }

        private void PressButtonChooseHero()
        {
            eventBus.Invoke(new SignalPressButtonChooseHero(soHero, this));
        }

    }
}
