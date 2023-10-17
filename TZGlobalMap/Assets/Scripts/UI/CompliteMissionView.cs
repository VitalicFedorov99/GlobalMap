using UnityEngine;
using TMPro;
using UnityEngine.UI;

using GlobalMap.Architecture;
using GlobalMap.Signals;

namespace GlobalMap.UI
{
    [System.Serializable]
    public class CompliteMissionView
    {
        [SerializeField] private Button buttonEndMission;
        [SerializeField] private TMP_Text nameMission;
        [SerializeField] private Image imageMission;
        [SerializeField] private RectTransform imageObject;
        [SerializeField] private TMP_Text descriptionPlayer;
        [SerializeField] private TMP_Text descriptionEnemy;
        [SerializeField] private TMP_Text descriptionMission;

        private EventBus eventBus;
        private CollectionImageMission collectionImages;

        public void Setup(EventBus bus,CollectionImageMission collection)
        {
            eventBus = bus;
            buttonEndMission.onClick.RemoveAllListeners();
            buttonEndMission.onClick.AddListener(EndMission);
            collectionImages = collection;
            RegisterEvent();
        }

        private void RegisterEvent() 
        {
            eventBus.Subscribe<SignalEndMission>(OpenDisplay);
        }
            
        private void EndMission() 
        {
            imageObject.gameObject.SetActive(false);
            eventBus.Invoke(new SignalPressButtonCompliteMission());
            eventBus.Invoke(new SignalCheckOpenNewHero());
        }

        private void OpenDisplay(SignalEndMission signal)
        {
            var mission = signal.CurrentMission.GetMissionData();
            imageObject.gameObject.SetActive(true);
            nameMission.text = mission.NameMission;
            descriptionEnemy.text = mission.EnemyText;
            descriptionPlayer.text = mission.PlayerText;
            descriptionMission.text = mission.ActionText;
            imageMission.sprite = collectionImages.GetImageMission(mission.Number).Icon;
        }
    }
}
