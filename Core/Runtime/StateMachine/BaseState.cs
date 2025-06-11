using System;
using UnityEngine;

namespace JellyFramework.StateMachine
{
    public abstract class BaseState
    {
        //public abstract void Init(BaseStateMachine machine);
        public abstract void EnterState(params object[] data);
        public abstract void UpdateState(float deltaTime, float timeScale);
        public abstract void ExitState();
    }
}
