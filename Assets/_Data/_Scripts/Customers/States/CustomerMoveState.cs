using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerMoveState : CustomerState
    {
        public CustomerMoveState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void Enter()
        {
            base.Enter();

            customer.aiDestinationSetter.target = customer.targetTransform;
        }
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (customer.aiPath.reachedDestination && customer.targetChair != null)
            {
                customer.targetChair.SetChairStatus(ChairStatus.Occupied);
                stateMachine.ChangeState(customer.SittingState);
            }

            if (customer.aiPath.reachedDestination && customer.targetChair == null)
            {
                stateMachine.ChangeState(customer.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            customer.aiDestinationSetter.target = null;
            customer.targetTransform = null;
        }
    }
}