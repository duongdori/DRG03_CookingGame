using UnityEngine;

namespace AssistantChefs
{
    public class AssistantChefIdleState : AssistantChefState
    {
        public AssistantChefIdleState(StateMachine stateMachine, string animBoolName, AssistantChefBehaviour assistantChef) : base(stateMachine, animBoolName, assistantChef)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (assistantChef.targetTransform != null)
            {
                assistantChef.isFree = false;
                stateMachine.ChangeState(assistantChef.MoveState);
            }
        }
    }
}