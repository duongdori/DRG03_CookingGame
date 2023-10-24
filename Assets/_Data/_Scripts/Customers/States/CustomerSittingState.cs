using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerSittingState : CustomerState
    {
        public CustomerSittingState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (customer.targetChair != null && customer.targetTable.TableStatus == TableStatus.FoodServed)
            {
                stateMachine.ChangeState(customer.EatingState);
            }
            else if (customer.targetChair != null && customer.targetTable.TableStatus == TableStatus.Empty)
            {
                customer.targetChair.SetChairStatus(ChairStatus.Empty);
                customer.targetChair = null;
                customer.targetTable = null;
                customer.targetTransform = Door.Instance.transform;
                stateMachine.ChangeState(customer.IdleState);
            }
        }
    }
}