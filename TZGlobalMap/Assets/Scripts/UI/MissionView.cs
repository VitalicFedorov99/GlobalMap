using UnityEngine;

using TMPro;
using UnityEngine.UI;

using GlobalMap.Architecture;
using GlobalMap.Signals;
using GlobalMap.Map;

namespace GlobalMap.UI
{
    [System.Serializable]
    public class MissionView
    {
        [SerializeField] private TMP_Text nameText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Image imageMission;
        [SerializeField] private RectTransform imageObject;
        [SerializeField] private Button buttonStartMission;
        [SerializeField] private Button buttonCloseMission;
        [SerializeField] private Image blockImage;


        private EventBus eventBus;
        private MissionBuilder currentMission;
        private CollectionImageMission collectionImages;

        public void Setup(EventBus bus, CollectionImageMission collection)
        {
            eventBus = bus;
            collectionImages = collection;
            RegisterEvents();
            buttonStartMission.onClick.RemoveAllListeners();
            buttonStartMission.onClick.AddListener(PressButtonStartGame);
            buttonCloseMission.onClick.RemoveAllListeners();
            buttonCloseMission.onClick.AddListener(Close);
        }

        private void RegisterEvents()
        {
            eventBus.Subscribe<SignalOpenMission>(OpenMission);
            eventBus.Subscribe<SignalPressButtonStart>(Close);
            eventBus.Subscribe<SignalNewStep>(StartNewStep);
            eventBus.Subscribe<SignalRemoveBlockButtonStartMission>(BlockImageOff);
        }
        private void OpenMission(SignalOpenMission signal) => OpenDisplay(signal.CurrentMission);
        private void StartNewStep(SignalNewStep signal)
        {
            imageObject.gameObject.SetActive(false);
            blockImage.gameObject.SetActive(true);
        }

        private void OpenDisplay(MissionBuilder mission)
        {
            var data = mission.GetMissionData();
            imageObject.gameObject.SetActive(true);
            nameText.text = data.NameMission;
            descriptionText.text = data.Description;
            currentMission = mission;
            imageMission.sprite = collectionImages.GetImageMission(data.Number).Icon;
        }

        private void Close()
        {
            eventBus.Invoke(new SignalPressButtonCloseMission(currentMission));
            imageObject.gameObject.SetActive(false);
        }

        private void Close(SignalPressButtonStart signal) => imageObject.gameObject.SetActive(false);
        private void BlockImageOff(SignalRemoveBlockButtonStartMission signal) => blockImage.gameObject.SetActive(false);

        private void PressButtonStartGame()
        {
            eventBus.Invoke(new SignalStartMission());
            eventBus.Invoke(new SignalPressButtonStart(currentMission));
        }


    }
}
