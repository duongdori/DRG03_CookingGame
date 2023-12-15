using System;
using Kitchen;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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

        public GameObject orderIcon;
        public GameObject moneyIcon;
        public SpriteRenderer spriteRenderer;

        public GameObject kimchi;
        public Cook cook;
        public FoodTray foodTray;
        public FoodHolder foodHolder;
        
        public bool hasStaff = false;
        public bool staffArrived = false;

        public float time = 0f;
        public float timeCheck = 0f;

        public event UnityAction<Table, TableStatus> OnTableStatusChanged;
        public event UnityAction<bool> OnStaffArrived;
        private void Awake()
        {
            chairManager = GetComponentInChildren<ChairManager>();
            foodHolder = GetComponentInChildren<FoodHolder>();
            kimchi.SetActive(false);
        }

        private void OnEnable()
        {
            Save();
        }

        private void Update()
        {
            if (chairManager.AreAllCustomersSeated() && tableStatus == TableStatus.Reserved)
            {
                timeCheck = 0f;
                SetTableStatus(TableStatus.PendingToOrder);
                orderIcon.SetActive(true);
                SoundManager.Instance.PlaySfx(Sound.Order);
            }
            else if (tableStatus == TableStatus.FoodServed)
            {
                timeCheck = 0f;
                if (time > 10f)
                {
                    time = 0f;
                    SetTableStatus(TableStatus.PaymentRequested);
                    moneyIcon.SetActive(true);
                }

                time += Time.deltaTime;
            }

            if (tableStatus == TableStatus.PendingToOrder && hasStaff)
            {
                timeCheck += Time.deltaTime;
                if (timeCheck >= 18f)
                {
                    timeCheck = 0f;
                    SetHasStaff(false);
                }
            }
            else if (tableStatus == TableStatus.PaymentRequested && hasStaff)
            {
                timeCheck += Time.deltaTime;
                if (timeCheck >= 18f)
                {
                    timeCheck = 0f;
                    SetHasStaff(false);
                }
            }
            
            
        }

        public void SetupTable(Sprite tableSprite, Sprite rightChair, Sprite leftChair)
        {
            spriteRenderer.sprite = tableSprite;
            chairManager.SetupChair(rightChair, leftChair);
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
            kimchi.SetActive(true);
        }

        public void RemoveFood()
        {
            foreach (Food food in foodHolder.holder)
            {
                food.Setup(null, "FoodPoint");
            }
            kimchi.SetActive(false);
        }

        private void Save()
        {
            ES3.Save(transform.name, transform);
        }
        
        private void OnApplicationQuit()
        {
            Save();
        }
    }
}
