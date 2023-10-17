using UnityEngine;
using UnityEngine.EventSystems;

using GlobalMap.Architecture;
using GlobalMap.Signals;


namespace GlobalMap.Map
{
    public class MissionBuilder : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private MissionData data;
        private TMPro.TMP_Text textNumber;
        private ColorizeComponent colorizeComponent;
        private StateMissionController stateMissionController;

        private EventBus eventBus;

        private bool missionChoose;

        public void Setup(MissionData missionData, TMPro.TMP_Text text, EventBus bus)
        {
            data = missionData;
            textNumber = text;
            textNumber.text = missionData.Number.ToString();
            eventBus = bus;
            RegisterEvents();
            colorizeComponent = new ColorizeComponent(GetComponent<SpriteRenderer>());
            stateMissionController = new StateMissionController(this, bus);
            stateMissionController.Initialize();
        }

        public MissionData GetMissionData() => data;
        public ColorizeComponent GetColorizeComponent() => colorizeComponent;




        private void RegisterEvents()
        {
            eventBus.Subscribe<SignalOpenMission>(ChooseMission);
            eventBus.Subscribe<SignalStateBlockMission>(BlockMission);
            eventBus.Subscribe<SignalStateActivateMission>(ActiveMission);
            eventBus.Subscribe<SignalStateTimeDeactivateMission>(TimeDisactiveMission);
            eventBus.Subscribe<SignalStateCompliteMission>(CompliteMission);
            eventBus.Subscribe<SignalPressButtonCloseMission>(CancelChooseMission);
            eventBus.Subscribe<SignalRemoveLinks>(RemoveLinks);
        }



        private void PlacementText()
        {
            if (textNumber == null)
                return;

          
            Vector3 worldPosition = transform.position;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            textNumber.rectTransform.position = screenPosition;
        }

        private void Update()
        {
            PlacementText();
        }
     

        #region Signals

        private void ChooseMission(SignalOpenMission signal) => missionChoose = true;

        private void BlockMission(SignalStateBlockMission signal)
        {
            if (signal.CurrentMission != this)
                return;

            gameObject.SetActive(false);
            textNumber.gameObject.SetActive(false);
            stateMissionController.ChangeState(StateMission.Block);
            eventBus.Invoke(new SignalRemoveLinks(this));
        }

        private void RemoveLinks(SignalRemoveLinks signal)
        {
            if (!data.CheckPrevMission(signal.CurrentMission.GetMissionData().Number))
                return;

            data.RemovePrevLink(signal.CurrentMission.GetMissionData().Number);
            if (data.CheckPrevMission())
            {
                eventBus.Invoke(new SignalStateActivateMission(this));
            }
            if (data.CheckNullPrevMission())
            {
                eventBus.Invoke(new SignalStateBlockMission(this));
            }


        }

        private void ActiveMission(SignalStateActivateMission signal)
        {
            if (signal.CurrentMission != this)
                return;


            gameObject.SetActive(true);
            textNumber.gameObject.SetActive(true);
            stateMissionController.ChangeState(StateMission.Active);


        }

        private void TimeDisactiveMission(SignalStateTimeDeactivateMission signal)
        {
            if (signal.CurrentMission != this)
                return;

            stateMissionController.ChangeState(StateMission.TimeDisactive);


        }

        private void CompliteMission(SignalStateCompliteMission signal)
        {
            missionChoose = false;
            if (signal.CurrentMission != this)
                return;

            stateMissionController.ChangeState(StateMission.Complite);
        }

        private void CancelChooseMission(SignalPressButtonCloseMission signal)
        {
            missionChoose = false;
            if (signal.CurrentMission != this)
                return;

            stateMissionController.ChangeState(StateMission.Active);
        }

        #endregion

        #region Pointer
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!missionChoose)
                stateMissionController.CurrentState.PointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!missionChoose)
                stateMissionController.CurrentState.PointerExit();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!missionChoose)
                stateMissionController.CurrentState.PointerClick();
        }
        #endregion
    }
}
