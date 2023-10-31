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

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (staff.aiPath.velocity.x < 0)
            {
                Vector3 newScale = staff.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * -1;
                staff.transform.localScale = newScale;
            }
            else
            {
                Vector3 newScale = staff.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x);
                staff.transform.localScale = newScale;
            }
        }

        public override void Exit()
        {
            base.Exit();
            // staff.anim.SetBool(animBoolName, false);
        }
    }
}