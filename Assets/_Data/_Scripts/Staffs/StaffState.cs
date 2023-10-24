using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffState : BaseState
    {
        protected StaffBehaviour staff;
        
        public StaffState(StateMachine stateMachine, string animBoolName, StaffBehaviour staff) : base(stateMachine, animBoolName)
        {
            this.staff = staff;
        }
        
        public override void Enter()
        {
            base.Enter();
            // staff.anim.SetBool(animBoolName, true);
        }

        public override void Exit()
        {
            base.Exit();
            // staff.anim.SetBool(animBoolName, false);
        }
    }
}