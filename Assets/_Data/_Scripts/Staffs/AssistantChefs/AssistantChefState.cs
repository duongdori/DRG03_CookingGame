using UnityEngine;

namespace AssistantChefs
{
    public class AssistantChefState : BaseState
    {
        protected AssistantChefBehaviour assistantChef;
        
        public AssistantChefState(StateMachine stateMachine, string animBoolName, AssistantChefBehaviour assistantChef) : base(stateMachine, animBoolName)
        {
            this.assistantChef = assistantChef;
        }
        
        public override void Enter()
        {
            base.Enter();
            // assistantChef.anim.SetBool(animBoolName, true);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if (assistantChef.aiPath.velocity.x < 0)
            {
                Vector3 newScale = assistantChef.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x) * -1;
                assistantChef.transform.localScale = newScale;
            }
            else
            {
                Vector3 newScale = assistantChef.transform.localScale;
                newScale.x = Mathf.Abs(newScale.x);
                assistantChef.transform.localScale = newScale;
            }
        }

        public override void Exit()
        {
            base.Exit();
            // assistantChef.anim.SetBool(animBoolName, false);
        }
    }
}