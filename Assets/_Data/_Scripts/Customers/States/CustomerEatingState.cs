using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerEatingState : CustomerState
    {
        public CustomerEatingState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (customer.targetChair != null && customer.targetTable.TableStatus == TableStatus.PaymentRequested)
            {
                stateMachine.ChangeState(customer.SittingState);
            }
        }
    }
}