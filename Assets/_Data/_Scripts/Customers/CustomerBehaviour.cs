using Pathfinding;
using TableAndChair;
using UnityEngine;

namespace Customers
{
    public class CustomerBehaviour : MonoBehaviour
    {
        public Animator anim;
        public AIPath aiPath;
        public AIDestinationSetter aiDestinationSetter;

        public Chair targetChair;
        public Table targetTable;
        public Transform targetTransform;

        private StateMachine _stateMachine;
        public CustomerIdleState IdleState { get; private set; }
        public CustomerMoveState MoveState { get; private set; }
        public CustomerSittingState SittingState { get; private set; }
        public CustomerEatingState EatingState { get; private set; }

        private void Awake()
        {
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

        private void Update()
        {
            _stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }
    }
}
