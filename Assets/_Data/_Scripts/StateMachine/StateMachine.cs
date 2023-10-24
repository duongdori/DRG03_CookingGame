
public class StateMachine
{
    public BaseState CurrentState { get; private set; }

    public void Initialize(BaseState startingBaseState)
    {
        CurrentState = startingBaseState;
        CurrentState.Enter();
    }
    public void ChangeState(BaseState newBaseState)
    {
        CurrentState.Exit();
        CurrentState = newBaseState;
        CurrentState.Enter();
    }
}

