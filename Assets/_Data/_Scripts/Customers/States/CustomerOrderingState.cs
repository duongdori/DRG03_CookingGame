using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerOrderingState : CustomerState
    {
        public CustomerOrderingState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void Enter()
        {
            base.Enter();
            FoodData foodData = MenuManager.Instance.GetRandomFoodFromMenu();
            customer.targetTable.AddFood(foodData);
            customer.SetupPopupText(foodData.name, true);
            
            customer.targetTable.OnTableStatusChanged += TargetTableOnOnTableStatusChanged;
        }

        private void TargetTableOnOnTableStatusChanged(Table table, TableStatus status)
        {
            if(table != customer.targetTable) return;

            if (status == TableStatus.Occupied)
            {
                stateMachine.ChangeState(customer.SittingState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            customer.SetupPopupText("", false);
        }
    }
}