using Kitchen;
using UnityEngine;
using UnityEngine.Events;

namespace TableAndChair
{
    public enum TableStatus
    {
        Empty,
        Reserved,
        WaitingForOrder,
        Occupied,
        FoodServed,
        PaymentRequested,
        BillPaid
    }
    public class Table : MonoBehaviour
    {
        [SerializeField] private TableStatus tableStatus = TableStatus.Empty;
        public TableStatus TableStatus => tableStatus;
        
        [SerializeField] private ChairManager chairManager;
        public ChairManager ChairManager => chairManager;

        [SerializeField] private Transform staffPoint;
        public Transform StaffPoint => staffPoint;

        public Transform assistantChefPoint;

        public FoodTray foodTray;
        
        public bool hasStaff = false;

        public float time = 0f;

        public event UnityAction<Table, TableStatus> OnTableStatusChanged; 
        private void Awake()
        {
            chairManager = GetComponentInChildren<ChairManager>();
        }

        private void Update()
        {
            if (chairManager.AreAllCustomersSeated() && tableStatus == TableStatus.Reserved)
            {
                SetTableStatus(TableStatus.WaitingForOrder);
            }
            else if (tableStatus == TableStatus.FoodServed)
            {
                if (time > 5f)
                {
                    time = 0f;
                    SetTableStatus(TableStatus.PaymentRequested);
                }

                time += Time.deltaTime;
            }
            
        }

        public void SetTableStatus(TableStatus status)
        {
            tableStatus = status;
            OnTableStatusChanged?.Invoke(this, tableStatus);
        }

        public void SetHasStaff(bool value)
        {
            hasStaff = value;
        }

        public void DestroyFoodTray()
        {
            if(foodTray == null) return;
            Destroy(foodTray.gameObject);
            foodTray = null;
        }
    }
}
