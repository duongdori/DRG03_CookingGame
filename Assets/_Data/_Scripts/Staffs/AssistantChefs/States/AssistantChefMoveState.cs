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

            if (assistantChef.aiPath.reachedDestination && assistantChef.foodTray != null)
            {
                assistantChef.modelHasFood.SetActive(false);
                assistantChef.modelNoFood.SetActive(true);
                
                assistantChef.targetTable.foodTray = assistantChef.foodTray;
                assistantChef.targetTable.SetupFood();
                assistantChef.targetTable.SetTableStatus(TableStatus.FoodServed);
                assistantChef.foodTray = null;
                assistantChef.targetTransform = assistantChef.idlePoint;
                stateMachine.ChangeState(assistantChef.IdleState);
            }
            else if (assistantChef.aiPath.reachedDestination && assistantChef.foodTray == null)
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