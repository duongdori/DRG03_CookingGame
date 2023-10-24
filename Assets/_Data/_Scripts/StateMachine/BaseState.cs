using UnityEngine;

public class BaseState
{
    protected StateMachine stateMachine;
    
    protected readonly string animBoolName;
    
    protected bool isAnimationFinished;
    protected bool isExitingState;
    protected float startTime;

    public BaseState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        DoChecks();
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        Debug.Log(animBoolName);
    }

    public virtual void Exit()
    {
        isExitingState = true;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }

    protected virtual void DoChecks()
    {
    }

    public virtual void AnimationTrigger()
    {
    }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;
}
