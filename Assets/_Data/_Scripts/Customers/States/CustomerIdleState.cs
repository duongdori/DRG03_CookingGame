using UnityEngine;

namespace Customers
{
    public class CustomerIdleState : CustomerState
    {
        public CustomerIdleState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void Enter()
        {
            base.Enter();
            customer.sitModel.SetActive(false);
            customer.normalModel.SetActive(true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (customer.targetTransform != null)
            {
                stateMachine.ChangeState(customer.MoveState);
            }
            else
            {
                customer.customerHolder.ReturnCustomerToPool(customer);
            }
        }
    }
}