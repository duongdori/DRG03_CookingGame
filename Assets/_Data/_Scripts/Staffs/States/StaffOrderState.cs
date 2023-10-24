using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffOrderState : StaffState
    {
        public StaffOrderState(StateMachine stateMachine, string animBoolName, StaffBehaviour staff) : base(stateMachine, animBoolName, staff)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (Time.time > startTime + 5f)
            {
                staff.targetTransform = staff.idlePoint;
                stateMachine.ChangeState(staff.IdleState);
            }
        }

        public override void Exit()
        {
            base.Exit();
            staff.targetTable.SetTableStatus(TableStatus.Occupied);
            staff.targetTable.SetHasStaff(false);
            staff.targetTable = null;
        }
    }
}