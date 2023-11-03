using Kitchen;
using UnityEngine;
using UnityEngine.Events;

namespace TableAndChair
{
    public enum TableStatus
    {
        Empty,
        Reserved,
        PendingToOrder,
        Ordering,
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

        public Cook cook;
        public FoodTray foodTray;
        public FoodHolder foodHolder;
        
        public bool hasStaff = false;
        public bool staffArrived = false;

        public float time = 0f;

        public event UnityAction<Table, TableStatus> OnTableStatusChanged;
        public event UnityAction<bool> OnStaffArrived;
        private void Awake()
        {
            chairManager = GetComponentInChildren<ChairManager>();
            foodHolder = GetComponentInChildren<FoodHolder>();
        }

        private void Update()
        {
            if (chairManager.AreAllCustomersSeated() && tableStatus == TableStatus.Reserved)
            {
                SetTableStatus(TableStatus.PendingToOrder);
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

        public bool IsEmptyTable()
        {
            return tableStatus == TableStatus.Empty && chairManager.IsEmptyChairs();
        }

        public bool IsPendingToOrder()
        {
            return tableStatus == TableStatus.PendingToOrder && !hasStaff;
        }
        
        public bool IsPaymentRequested()
        {
            return tableStatus == TableStatus.PaymentRequested && !hasStaff;
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
        
        public void SetStaffArrived(bool value)
        {
            staffArrived = value;
            OnStaffArrived?.Invoke(value);
        }

        public void SetCook(Cook cook)
        {
            this.cook = cook;
        }

        public void AddFood(FoodData foodData)
        {
            cook.AddFood(foodData);
        }

        public void DestroyFoodTray()
        {
            foodTray = null;
        }

        public void SetupFood()
        {
            if(foodTray == null) return;

            for (int i = 0; i < foodTray.foodList.Count; i++)
            {
                foodHolder.holder[i].Setup(foodTray.foodList[i].sprite, foodTray.foodList[i].name);
            }
        }

        public void RemoveFood()
        {
            foreach (Food food in foodHolder.holder)
            {
                food.Setup(null, "FoodPoint");
            }
        }
    }
}
