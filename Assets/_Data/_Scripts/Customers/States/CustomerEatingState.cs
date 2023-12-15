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
            customer.anim.SetBool(animBoolName, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(isExitingState) return;
            if (customer.targetTable != null && customer.targetTable.TableStatus == TableStatus.PaymentRequested)
            {
                stateMachine.ChangeState(customer.SittingState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            customer.anim.SetBool(animBoolName, false);
        }
    }
}