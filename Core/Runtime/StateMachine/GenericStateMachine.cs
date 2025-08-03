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
        protected TState currentState;
        public TState CurrentState => currentState;

        public void InitStates() => states.Iterate(x => x.Init(this));

        public void ChangeState(TType type, params object[] data)
        {
            TState newState = states.Find((state) => state.Type.Equals(type));
            currentState?.ExitState();
            currentState = newState;
            currentState?.EnterState(data);
        }

        public void Update(float deltaTime, float timeScale) => currentState?.UpdateState(deltaTime, timeScale);
    }
}

