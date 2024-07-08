namespace Infrastructure
{
    public interface IState : IExitableState
    {
        void Enter();
        void Exit();
    }
}