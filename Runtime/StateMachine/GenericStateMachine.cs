using System;
using UnityEngine;
using JellyFramework.ExtensionMethod;

namespace JellyFramework.StateMachine
{
    public class GenericStateMachine<TMachine, TState, TType, TData> : BaseStateMachine
        where TMachine : BaseStateMachine
        where TState : GenericState<TMachine, TState, TType, TData>
        where TType : Enum
    {
        [SerializeField] private TState[] states;
        private TState curState;

        private void Awake() => states.Iterate(x => x.Init(this));

        public void ChangeState(TType type, TData data = default)
        {
            TState newState = Array.Find(states, (state) => state.Type.Equals(type));
            curState?.ExitState();
            curState = newState;
            curState?.EnterState(data);
        }

        public void CustomUpdate(float deltaTime, float timeScale) => curState?.UpdateState(deltaTime, timeScale);
    }
}

