using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffMoveState : StaffState
    {
        public StaffMoveState(StateMachine stateMachine, string animBoolName, StaffBehaviour staff) : base(stateMachine, animBoolName, staff)
        {
        }

        public override void Enter()
        {
            base.Enter();
            staff.modelIdle.SetActive(false);
            staff.modelMove.SetActive(true);
            
            staff.aiDestinationSetter.target = staff.targetTransform;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (staff.aiPath.reachedDestination && staff.targetTable != null 
                                                && staff.targetTable.TableStatus == TableStatus.PendingToOrder)
            {
                stateMachine.ChangeState(staff.OrderState);
            }
            else if (staff.aiPath.reachedDestination && staff.targetTable != null &&
                     staff.targetTable.TableStatus == TableStatus.PaymentRequested)
            {
                stateMachine.ChangeState(staff.BillingState);
            }
            else if (staff.aiPath.reachedDestination && staff.targetTable == null)
            {
                staff.SetIsFree(true);
                stateMachine.ChangeState(staff.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            staff.aiDestinationSetter.target = null;
            staff.targetTransform = null;
            
            staff.modelIdle.SetActive(true);
            staff.modelMove.SetActive(false);
        }
    }
}