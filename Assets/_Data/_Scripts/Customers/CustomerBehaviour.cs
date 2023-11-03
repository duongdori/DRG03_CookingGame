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
        
        [Header("Models")]
        public GameObject normalModel;
        public GameObject sitModel;
        public PopupText popupText;
        
        [Header("Target")]
        public Chair targetChair;
        public Table targetTable;
        public Transform targetTransform;
        
        private StateMachine _stateMachine;
        public CustomerIdleState IdleState { get; private set; }
        public CustomerMoveState MoveState { get; private set; }
        public CustomerSittingState SittingState { get; private set; }
        public CustomerOrderingState OrderingState { get; private set; }
        public CustomerEatingState EatingState { get; private set; }
        
        protected override void Awake()
        {
            base.Awake();
            
            customerHolder = GetComponentInParent<CustomerHolder>();
            
            _stateMachine = new StateMachine();
            IdleState = new CustomerIdleState(_stateMachine, "Idle", this);
            MoveState = new CustomerMoveState(_stateMachine, "Move", this);
            SittingState = new CustomerSittingState(_stateMachine, "Sitting", this);
            OrderingState = new CustomerOrderingState(_stateMachine, "Ordering", this);
            EatingState = new CustomerEatingState(_stateMachine, "Eating", this);
        }

        private void OnEnable()
        {
            _stateMachine.Initialize(IdleState);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadAnimator();
            LoadAIPath();
            LoadAIDestinationSetter();
            LoadNormalModel();
            LoadSitModel();
            LoadPopupText();
        }

        private void Update()
        {
            _stateMachine.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _stateMachine.CurrentState.PhysicsUpdate();
        }

        public void SetupPopupText(string text, bool isActive)
        {
            popupText.gameObject.SetActive(isActive);
            popupText.Setup(text);
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
        
        private void LoadPopupText()
        {
            if(popupText != null) return;
            popupText = GetComponentInChildren<PopupText>();
            popupText.gameObject.SetActive(false);
            Debug.LogWarning(transform.name + ": LoadPopupText", gameObject);
        }
        #endregion
    }
}
