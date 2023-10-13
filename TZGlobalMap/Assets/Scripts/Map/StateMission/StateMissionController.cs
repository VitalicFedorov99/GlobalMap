using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GlobalMap.Architecture;

namespace GlobalMap.Map
{
    public class StateMissionController
    {
        private IStateMission activeState;
        private IStateMission blockState;
        private IStateMission timeDisactiveState;
        private IStateMission compliteState;

        public IStateMission CurrentState { get; set; }

        public StateMissionController(MissionBuilder mission, EventBus bus)
        {
            activeState = new StateActive(mission, bus);
            blockState = new StateBlock(mission, bus);
            timeDisactiveState = new StateTimeDisactive(mission,bus);
            compliteState = new StateComplite(mission,bus);
        }

        public void Initialize()
        {
            CurrentState = activeState;
            CurrentState.Enter();
        }

        public void ChangeState(StateMission state)
        {
            CurrentState.Exit();
            CurrentState = state switch
            {
                StateMission.Active => activeState,
                StateMission.Block => blockState,
                StateMission.TimeDisactive => timeDisactiveState,
                StateMission.Complite => compliteState,
                _ => activeState,
            };
            CurrentState.Enter();
        }
    }

    public enum StateMission
    {
        Active,
        Block,
        TimeDisactive,
        Complite
    }

    public interface IStateMission
    {
        public void Enter();
        public void Exit();

        public void PointerEnter();

        public void PointerExit();

        public void PointerClick();
    }
}
