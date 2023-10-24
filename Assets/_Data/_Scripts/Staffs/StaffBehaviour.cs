using Pathfinding;
using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffBehaviour : MonoBehaviour
    {
        public Animator anim;
        public AIPath aiPath;
        public AIDestinationSetter aiDestinationSetter;
        
        public Transform targetTransform;
        public Transform idlePoint;
        public Table targetTable;
        
        public StateMachine StateMachine { get; private set; }
        public StaffIdleState IdleState { get; private set; }
        public StaffMoveState MoveState { get; private set; }
        public StaffOrderState OrderState { get; private set; }
        public StaffBillingState BillingState { get; private set; }

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();

            StateMachine = new StateMachine();
            IdleState = new StaffIdleState(StateMachine, "Idle", this);
            MoveState = new StaffMoveState(StateMachine, "Move", this);
            OrderState = new StaffOrderState(StateMachine, "Order", this);
            BillingState = new StaffBillingState(StateMachine, "Billing", this);
        }

        private void Start()
        {
            transform.position = idlePoint.position;
            StateMachine.Initialize(IdleState);
        }

        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
    }
}