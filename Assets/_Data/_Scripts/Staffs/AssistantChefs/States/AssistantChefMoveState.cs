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
                
                assistantChef.foodTray.gameObject.SetActive(true);
                assistantChef.foodTray.transform.SetParent(assistantChef.foodTray.targetTable.transform);
                assistantChef.foodTray.transform.localPosition = Vector3.zero;
                assistantChef.targetTable.foodTray = assistantChef.foodTray;
                assistantChef.foodTray.targetTable.SetTableStatus(TableStatus.FoodServed);
                assistantChef.foodTray = null;
                assistantChef.targetTransform = assistantChef.idlePoint;
                stateMachine.ChangeState(assistantChef.IdleState);
            }
            else if (assistantChef.aiPath.reachedDestination && assistantChef.foodTray == null)
            {
                assistantChef.modelHasFood.SetActive(true);
                assistantChef.modelNoFood.SetActive(false);
                
                assistantChef.targetTransform = null;
                assistantChef.isFree = true;
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