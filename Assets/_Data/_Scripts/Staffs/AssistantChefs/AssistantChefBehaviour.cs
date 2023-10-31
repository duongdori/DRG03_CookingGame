using Kitchen;
using Pathfinding;
using TableAndChair;
using UnityEngine;

namespace AssistantChefs
{
    public class AssistantChefBehaviour : MyMonobehaviour
    {
        public Animator anim;
        public AIPath aiPath;
        public AIDestinationSetter aiDestinationSetter;

        public Transform idlePoint;
        public Transform targetTransform;
        public Table targetTable;
        public FoodTray foodTray;
        
        public GameObject modelHasFood;
        public GameObject modelNoFood;

        public bool isFree = true;
        
        public StateMachine StateMachine { get; private set; }
        public AssistantChefIdleState IdleState { get; private set; }
        public AssistantChefMoveState MoveState { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            anim = GetComponentInChildren<Animator>();
            aiPath = GetComponent<AIPath>();
            aiDestinationSetter = GetComponent<AIDestinationSetter>();

            StateMachine = new StateMachine();
            IdleState = new AssistantChefIdleState(StateMachine, "Idle", this);
            MoveState = new AssistantChefMoveState(StateMachine, "Move", this);
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
            LoadModelHasFood();
            LoadModelNoFood();
        }

        private void Update()
        {
            StateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            StateMachine.CurrentState.PhysicsUpdate();
        }
        
        private void LoadModelHasFood()
        {
            if(modelHasFood != null) return;
            modelHasFood = transform.GetChild(0).gameObject;
            Debug.LogWarning(transform.name + ": LoadModelHasFood", gameObject);
        }
        private void LoadModelNoFood()
        {
            if(modelNoFood != null) return;
            modelNoFood = transform.GetChild(1).gameObject;
            modelNoFood.SetActive(false);
            Debug.LogWarning(transform.name + ": LoadModelNoFood", gameObject);
        }
    }
}