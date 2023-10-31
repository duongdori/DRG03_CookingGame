using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerEatingState : CustomerState
    {
        public CustomerEatingState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void Enter()
        {
            base.Enter();
            customer.targetTable.OnTableStatusChanged += TargetTableOnOnTableStatusChanged;
        }

        private void TargetTableOnOnTableStatusChanged(Table table, TableStatus status)
        {
            if(customer.targetChair == null) return;
            if(customer.targetTable != table) return;
            
            if (status == TableStatus.PaymentRequested)
            {
                stateMachine.ChangeState(customer.SittingState);
            }
        }
    }
}