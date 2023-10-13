using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private bool missionChoose;

        public void Setup(MissionData missionData, TMPro.TMP_Text text, EventBus bus)
        {
            data = missionData;
            textNumber = text;
            PlacementText();
            textNumber.text = missionData.Number.ToString();
            colorizeComponent = new ColorizeComponent(GetComponent<SpriteRenderer>(), Color.green);
            eventBus = bus;
            RegisterEvents();
            stateMissionController = new StateMissionController(this, bus);
            stateMissionController.Initialize();
        }




        public void ChangeState(StateMission state) => stateMissionController.ChangeState(state);
        public MissionData GetMissionData() => data;
        public ColorizeComponent GetColorizeComponent() => colorizeComponent;




        private void RegisterEvents()
        {
            eventBus.Subscribe<SignalOpenMission>(ChooseMission);
            eventBus.Subscribe<SignalBlockMission>(BlockMission);
            eventBus.Subscribe<SignalActiveMission>(ActiveMission);
            eventBus.Subscribe<SignalTimeDisactiveMission>(TimeDisactiveMission);
            eventBus.Subscribe<SignalCompliteMission>(CompliteMission);
            eventBus.Subscribe<SignalPressButtonCloseMission>(CancelChooseMission);
            eventBus.Subscribe<SignalRemoveLinks>(RemoveLinks);
        }



        private void PlacementText()
        {
            Vector3 worldPosition = transform.position;
            worldPosition.x += 0f;
            Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
            textNumber.rectTransform.position = screenPosition;
        }



        #region Signals

        private void ChooseMission(SignalOpenMission signal)
        {
            missionChoose = true;
        }

        private void BlockMission(SignalBlockMission signal)
        {
            if (signal.CurrentMission == this)
            {
                gameObject.SetActive(false);
                textNumber.gameObject.SetActive(false);
                stateMissionController.ChangeState(StateMission.Block);
                eventBus.Invoke(new SignalRemoveLinks(this));
            }
        }

        private void RemoveLinks(SignalRemoveLinks signal) 
        {
            if (data.CheckPrevMission(signal.CurrentMission.GetMissionData().Number)) 
            {
                data.RemovePrevLink(signal.CurrentMission.GetMissionData().Number);
                if(data.CheckNullPrevMission())
                {
                    eventBus.Invoke(new SignalBlockMission(this));
                }
            }
        }

        private void ActiveMission(SignalActiveMission signal)
        {
            if (signal.CurrentMission == this)
            {
                gameObject.SetActive(true);
                textNumber.gameObject.SetActive(true);
                stateMissionController.ChangeState(StateMission.Active);
            }
        }

        private void TimeDisactiveMission(SignalTimeDisactiveMission signal)
        {
           
            if (signal.CurrentMission == this)
            {
                stateMissionController.ChangeState(StateMission.TimeDisactive);
                
            }
            //Debug.LogError("Выбрали миссию");
            //missionChoose = true;
        }

        private void CompliteMission(SignalCompliteMission signal)
        {
            if (signal.CurrentMission == this)
            {
                stateMissionController.ChangeState(StateMission.Complite);
            }
            missionChoose = false;
        }

        private void CancelChooseMission(SignalPressButtonCloseMission signal)
        {
            if (signal.CurrentMission == this)
            {
                stateMissionController.ChangeState(StateMission.Active);
            }
            missionChoose = false;
        }

        #endregion



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
    }
}
