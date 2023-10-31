using Kitchen;
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
            
            Cook cook = KitchenManager.Instance.CreateCook();
            if(cook == null) return;
            
            cook.SetTargetTable(staff.targetTable);
            foreach (Chair chair in staff.targetTable.ChairManager.GetChairsOccupied())
            {
                FoodData foodData = MenuManager.Instance.GetRandomFoodFromMenu();
                cook.AddFood(foodData);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (Time.time > startTime + 3f)
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