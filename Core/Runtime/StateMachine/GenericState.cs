using System;
using UnityEngine;

namespace JellyFramework.StateMachine
{
    public abstract class GenericState<TMachine, TType> : BaseState
        where TMachine : BaseStateMachine
        where TType : Enum
    {
        protected TMachine machine;
        [SerializeReference] protected TType type;
        public TType Type => type;
        
        public override void Init(BaseStateMachine machine)
        {
            this.machine = machine as TMachine;
            Setup();
        }

        protected virtual void Setup() { }
    }
}

