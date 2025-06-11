using System;
using UnityEngine;
using JellyFramework.ExtensionMethod;
using System.Collections.Generic;

namespace JellyFramework.StateMachine
{
    public class GenericStateMachine<TMachine, TState, TType> : BaseStateMachine
        where TMachine : BaseStateMachine
        where TState : GenericState<TMachine, TType>
        where TType : Enum
    {
        [SerializeReference] protected List<TState> states;
        private TState curState;

        public void InitStates() => states.Iterate(x => x.Init(this));

        public void ChangeState(TType type, params object[] data)
        {
            TState newState = states.Find((state) => state.Type.Equals(type));
            curState?.ExitState();
            curState = newState;
            curState?.EnterState(data);
        }

        public void Update(float deltaTime, float timeScale) => curState?.UpdateState(deltaTime, timeScale);
    }
}

