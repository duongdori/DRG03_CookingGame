using TableAndChair;
using UnityEngine;

namespace AssistantChefs
{
    public class AssistantChefMoveState : AssistantChefState
    {
        public AssistantChefMoveState(StateMachine stateMachine, string animBoolName, AssistantChefBehaviour assistantChef) : base(stateMachine, animBoolName, assistantChef)
        {
        }

        public override void Enter()
        {
            base.Enter();
            assistantChef.aiDestinationSetter.target = assistantChef.targetTransform;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (assistantChef.aiPath.reachedDestination && assistantChef.targetTable != null)
            {
                stateMachine.ChangeState(assistantChef.DeliveryState);
            }
            else if (assistantChef.aiPath.reachedDestination && assistantChef.targetTable == null)
            {
                assistantChef.modelHasFood.SetActive(true);
                assistantChef.modelNoFood.SetActive(false);
                
                assistantChef.targetTransform = null;
                assistantChef.SetIsFree(true);
                stateMachine.ChangeState(assistantChef.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            assistantChef.aiDestinationSetter.target = null;
        }
    }
}