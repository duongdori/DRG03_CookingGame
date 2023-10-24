﻿using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffBillingState : StaffState
    {
        public StaffBillingState(StateMachine stateMachine, string animBoolName, StaffBehaviour staff) : base(stateMachine, animBoolName, staff)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time > startTime + 2f)
            {
                staff.targetTransform = staff.idlePoint;
                stateMachine.ChangeState(staff.IdleState);
            }
        }
        
        public override void Exit()
        {
            base.Exit();
            staff.targetTable.SetTableStatus(TableStatus.Empty);
            staff.targetTable.statusChanged = false;
            staff.targetTable.SetHasStaff(false);
            staff.targetTable = null;
        }
    }
}