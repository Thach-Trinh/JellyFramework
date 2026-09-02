namespace JellyFramework.StateMachine
{
    public interface IState<TType>
    {
        TType Type { get; }
        void EnterState(params object[] data);
        void UpdateState(float deltaTime, float timeScale);
        void ExitState();
    }
}
