using System;
using UnityEngine;

namespace JellyFramework.StateMachine
{
    public abstract class GenericState<TMachine, TState, TType, TData> : BaseState
        where TMachine : BaseStateMachine
        where TState : BaseState
        where TType : Enum
    {
        protected TMachine machine;
        [SerializeField] protected TType type;
        public TType Type => type;
        public override void Init(BaseStateMachine machine) => this.machine = machine as TMachine;
        public void GenericInit(TMachine machine) => this.machine = machine;
        public abstract void EnterState(TData data);
        public abstract void UpdateState(float deltaTime, float timeScale);
        public abstract void ExitState();
    }
}

