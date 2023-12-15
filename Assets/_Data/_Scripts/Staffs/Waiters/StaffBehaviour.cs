using System;
using Pathfinding;
using TableAndChair;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Staffs
{
    public class StaffBehaviour : MyMonobehaviour
    {
        public Animator anim;
        public AIPath aiPath;
        public AIDestinationSetter aiDestinationSetter;
        
        [Header("Target")]
        public Transform idlePoint;
        public Transform targetTransform;
        public Table targetTable;
        
        [Header("Models")]
        public GameObject modelIdle;
        public GameObject modelMove;

        [SerializeField] private bool isFree = true;
        
        public StateMachine StateMachine { get; private set; }
        public StaffIdleState IdleState { get; private set; }
        public StaffMoveState MoveState { get; private set; }
        public StaffOrderState OrderState { get; private set; }
        public StaffBillingState BillingState { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new StateMachine();
            IdleState = new StaffIdleState(StateMachine, "Idle", this);
            MoveState = new StaffMoveState(StateMachine, "Move", this);
            OrderState = new StaffOrderState(StateMachine, "Order", this);
            BillingState = new StaffBillingState(StateMachine, "Billing", this);
        }

        private void OnEnable()
        {
            Save();
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
            LoadAnimator();
            LoadAIPath();
            LoadAIDestinationSetter();
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

        public bool IsFreeStaff()
        {
            return StateMachine.CurrentState == IdleState && isFree;
        }

        public void SetIsFree(bool value)
        {
            isFree = value;
        }

        private void Save()
        {
            ES3.Save(transform.name, transform);
        }
        private void OnApplicationQuit()
        {
            Save();
        }

        #region LoadComponents
        private void LoadAnimator()
        {
            if(anim != null) return;
            anim = GetComponentInChildren<Animator>();
            Debug.LogWarning(transform.name + ": LoadAnimator", gameObject);
        }
        private void LoadAIPath()
        {
            if(aiPath != null) return;
            aiPath = GetComponent<AIPath>();
            Debug.LogWarning(transform.name + ": LoadAIPath", gameObject);
        }
        private void LoadAIDestinationSetter()
        {
            if(aiDestinationSetter != null) return;
            aiDestinationSetter = GetComponent<AIDestinationSetter>();
            Debug.LogWarning(transform.name + ": LoadAIDestinationSetter", gameObject);
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

        #endregion
        
    }
}