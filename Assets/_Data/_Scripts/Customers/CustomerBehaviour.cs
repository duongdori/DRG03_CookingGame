using Pathfinding;
using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerBehaviour : MyMonobehaviour
    {
        public CustomerHolder customerHolder;
        public Animator anim;
        public AIPath aiPath;
        public AIDestinationSetter aiDestinationSetter;

        public GameObject normalModel;
        public GameObject sitModel;

        public Chair targetChair;
        public Table targetTable;
        public Transform targetTransform;
        
        private StateMachine _stateMachine;
        public CustomerIdleState IdleState { get; private set; }
        public CustomerMoveState MoveState { get; private set; }
        public CustomerSittingState SittingState { get; private set; }
        public CustomerEatingState EatingState { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            customerHolder = GetComponentInParent<CustomerHolder>();
            anim = GetComponentInChildren<Animator>();
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();
            
            _stateMachine = new StateMachine();
            IdleState = new CustomerIdleState(_stateMachine, "Idle", this);
            MoveState = new CustomerMoveState(_stateMachine, "Move", this);
            SittingState = new CustomerSittingState(_stateMachine, "Sitting", this);
            EatingState = new CustomerEatingState(_stateMachine, "Eating", this);
        }

        private void OnEnable()
        {
            _stateMachine.Initialize(IdleState);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadNormalModel();
            LoadSitModel();
        }

        private void Update()
        {
            _stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }

        private void LoadNormalModel()
        {
            if(normalModel != null) return;
            normalModel = transform.GetChild(0).gameObject;
            Debug.LogWarning(transform.name + ": LoadNormalModel", gameObject);
        }
        private void LoadSitModel()
        {
            if(sitModel != null) return;
            sitModel = transform.GetChild(1).gameObject;
            sitModel.SetActive(false);
            Debug.LogWarning(transform.name + ": LoadSitModel", gameObject);
        }
    }
}
