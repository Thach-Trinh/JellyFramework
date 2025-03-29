using UnityEngine;

namespace JellyFramework.StateMachine
{
    public abstract class BaseState : MonoBehaviour
    {
        public abstract void Init(BaseStateMachine machine);
    }
}
