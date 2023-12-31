﻿using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffBillingState : StaffState
    {
        public StaffBillingState(StateMachine stateMachine, string animBoolName, StaffBehaviour staff) : base(stateMachine, animBoolName, staff)
        {
        }

        public override void Enter()
        {
            base.Enter();
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
            SoundManager.Instance.PlaySfx(Sound.BillPaid);
            staff.targetTable.moneyIcon.SetActive(false);
            staff.targetTable.SetTableStatus(TableStatus.Empty);
            GameManager.Instance.AddMoney(staff.targetTable.foodTray.GetPrice());
            staff.targetTable.DestroyFoodTray();
            staff.targetTable.RemoveFood();
            staff.targetTable.SetHasStaff(false);
            staff.targetTable = null;
            GameManager.Instance.AddExperience(40f);
        }
    }
}