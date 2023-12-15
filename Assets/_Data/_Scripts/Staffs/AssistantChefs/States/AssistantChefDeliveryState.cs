using Kitchen;
using TableAndChair;
using UnityEngine;

namespace AssistantChefs
{
    public class AssistantChefDeliveryState : AssistantChefState
    {
        public AssistantChefDeliveryState(StateMachine stateMachine, string animBoolName, AssistantChefBehaviour assistantChef) : base(stateMachine, animBoolName, assistantChef)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            assistantChef.modelHasFood.SetActive(false);
            assistantChef.modelNoFood.SetActive(true);

            assistantChef.targetTable.foodTray = assistantChef.foodTray;
            
            assistantChef.targetTable.SetupFood();
            assistantChef.targetTable.SetTableStatus(TableStatus.FoodServed);
            assistantChef.foodTray = null;
            assistantChef.targetTable = null;
            assistantChef.targetTransform = assistantChef.idlePoint;
            stateMachine.ChangeState(assistantChef.IdleState);
        }
    }
}