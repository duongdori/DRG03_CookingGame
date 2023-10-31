using Pathfinding;
using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffBehaviour : MyMonobehaviour
    {
        public Animator anim;
        public AIPath aiPath;
        public AIDestinationSetter aiDestinationSetter;
        
        public Transform targetTransform;
        public Transform idlePoint;
        public Table targetTable;
        
        public GameObject modelIdle;
        public GameObject modelMove;

        public bool isFree = true;
        
        public StateMachine StateMachine { get; private set; }
        public StaffIdleState IdleState { get; private set; }
        public StaffMoveState MoveState { get; private set; }
        public StaffOrderState OrderState { get; private set; }
        public StaffBillingState BillingState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
            anim = GetComponentInChildren<Animator>();
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();

            StateMachine = new StateMachine();
            IdleState = new StaffIdleState(StateMachine, "Idle", this);
            MoveState = new StaffMoveState(StateMachine, "Move", this);
            OrderState = new StaffOrderState(StateMachine, "Order", this);
            BillingState = new StaffBillingState(StateMachine, "Billing", this);
        }

        protected override void Start()
        {
            base.Start();
            
            transform.position = idlePoint.position;
            StateMachine.Initialize(IdleState);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadModelIdle();
            LoadModelMove();
        }

        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
        
        private void LoadModelIdle()
        {
            if(modelIdle != null) return;
            modelIdle = transform.GetChild(0).gameObject;
            Debug.LogWarning(transform.name + ": LoadModelIdle", gameObject);
        }
        private void LoadModelMove()
        {
            if(modelMove != null) return;
            modelMove = transform.GetChild(1).gameObject;
            modelMove.SetActive(false);
            Debug.LogWarning(transform.name + ": LoadModelMove", gameObject);
        }
    }
}