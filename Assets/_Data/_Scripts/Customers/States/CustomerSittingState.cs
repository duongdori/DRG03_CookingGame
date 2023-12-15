using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerSittingState : CustomerState
    {
        public CustomerSittingState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName, customer)
        {
        }

        public override void Enter()
        {
            base.Enter();
            customer.aiPath.enabled = false;
            customer.transform.position = customer.targetChair.sitPoint.position;
            customer.transform.localScale = customer.targetChair.sitPoint.localScale;
            customer.normalModel.SetActive(false);
            customer.sitModel.SetActive(true);
            customer.anim.SetBool(animBoolName, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(isExitingState) return;
            if (customer.targetTable != null && customer.targetTable.TableStatus == TableStatus.Ordering)
            {
                stateMachine.ChangeState(customer.OrderingState);
            }
            else if (customer.targetTable != null && customer.targetTable.TableStatus == TableStatus.FoodServed)
            {
                stateMachine.ChangeState(customer.EatingState);
            }
            else if (customer.targetTable != null && customer.targetTable.TableStatus == TableStatus.Empty)
            {
                customer.targetChair.SetChairStatus(ChairStatus.Empty);
                customer.targetChair = null;
                customer.targetTable = null;
                customer.targetTransform = Door.Instance.transform;
                customer.aiPath.enabled = true;
                stateMachine.ChangeState(customer.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            customer.anim.SetBool(animBoolName, false);
        }
    }
}