using System;

namespace JellyFramework.StateMachine
{
    public interface IStateMachine<TState, TType>
        where TState : IState<TType>
        where TType : Enum
    {
        TState[] States { get; }
        public TState CurrentState { get; set; }

        public void ChangeState(TType type, params object[] data)
        {
            TState newState = Array.Find(States, (state) => state.Type.Equals(type));
            CurrentState?.ExitState();
            CurrentState = newState;
            CurrentState?.EnterState(data);
        }

        public void Update(float deltaTime, float timeScale) => CurrentState?.UpdateState(deltaTime, timeScale);
    }
}

