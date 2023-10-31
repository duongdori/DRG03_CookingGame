using UnityEngine;

namespace Staffs
{
    public class StaffIdleState : StaffState
    {
        public StaffIdleState(StateMachine stateMachine, string animBoolName, StaffBehaviour staff) : base(stateMachine, animBoolName, staff)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (staff.targetTransform != null)
            {
                staff.isFree = false;
                stateMachine.ChangeState(staff.MoveState);
            }
        }
    }
}