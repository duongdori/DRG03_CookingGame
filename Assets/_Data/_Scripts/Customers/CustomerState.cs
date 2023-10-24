
namespace Customers
{
    public class CustomerState : BaseState
    {
        protected CustomerBehaviour customer;
        
        public CustomerState(StateMachine stateMachine, string animBoolName, CustomerBehaviour customer) : base(stateMachine, animBoolName)
        {
            this.customer = customer;
        }

        public override void Enter()
        {
            base.Enter();
            // customer.anim.SetBool(animBoolName, true);
        }

        public override void Exit()
        {
            base.Exit();
            // customer.anim.SetBool(animBoolName, false);
        }
    }
}
