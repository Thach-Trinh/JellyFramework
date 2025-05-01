using System;
using UnityEngine;
using JellyFramework.ExtensionMethod;

namespace JellyFramework.StateMachine
{
    public class GenericStateMachine<TMachine, TState, TType> : BaseStateMachine
        where TMachine : BaseStateMachine
        where TState : GenericState<TMachine, TState, TType>
        where TType : Enum
    {
        [SerializeField] private TState[] states;
        private TState curState;

        private void Awake() => states.Iterate(x => x.Init(this));

        public void ChangeState(TType type)
        {
            TState newState = Array.Find(states, (state) => state.Type.Equals(type));
            curState?.ExitState();
            curState = newState;
            curState?.EnterState();
        }

        public void ChangeState<TData>(TType type, TData data = default)
        {
            TState newState = Array.Find(states, (state) => state.Type.Equals(type));
            curState?.ExitState();
            curState = newState;
            if (curState is IInjector<TData> injector)
                injector.Inject(data);
            curState?.EnterState();
        }

        public void Tick(float deltaTime, float timeScale) => curState?.UpdateState(deltaTime, timeScale);
    }
}

