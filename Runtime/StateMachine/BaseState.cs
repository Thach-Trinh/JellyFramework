using UnityEngine;

namespace JellyFramework.StateMachine
{
    public abstract class BaseState : MonoBehaviour
    {
        public abstract void Init(BaseStateMachine machine);
        public abstract void EnterState();
        public abstract void UpdateState(float deltaTime, float timeScale);
        public abstract void ExitState();
    }

    public interface IInjector<T>
    {
        void Inject(T data);
    }
}
