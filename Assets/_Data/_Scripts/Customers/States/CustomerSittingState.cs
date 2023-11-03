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
            
            customer.targetTable.OnTableStatusChanged += TargetTableOnOnTableStatusChanged;
        }

        private void TargetTableOnOnTableStatusChanged(Table table, TableStatus status)
        {
            if(customer.targetTable != table) return;

            if (status == TableStatus.Ordering)
            {
                stateMachine.ChangeState(customer.OrderingState);
            }
            else if (status == TableStatus.FoodServed)
            {
                stateMachine.ChangeState(customer.EatingState);
            }
            else if (status == TableStatus.Empty)
            {
                customer.targetChair.SetChairStatus(ChairStatus.Empty);
                customer.targetChair = null;
                customer.targetTable = null;
                customer.targetTransform = Door.Instance.transform;
                customer.aiPath.enabled = true;
                stateMachine.ChangeState(customer.IdleState);
            }
        }
    }
}