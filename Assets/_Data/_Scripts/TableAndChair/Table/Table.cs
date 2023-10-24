using UnityEngine;
using UnityEngine.Serialization;

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

        public bool statusChanged = false;

        public bool hasStaff = false;

        public float time = 0f;
        private void Awake()
        {
            chairManager = GetComponentInChildren<ChairManager>();
        }

        private void Update()
        {
            if (chairManager.AreAllCustomersSeated() && !statusChanged)
            {
                statusChanged = true;
                tableStatus = TableStatus.WaitingForOrder;
            }

            if (tableStatus == TableStatus.Occupied)
            {
                if (time > 3f)
                {
                    time = 0f;
                    tableStatus = TableStatus.FoodServed;
                }

                time += Time.deltaTime;
            }

            if (tableStatus == TableStatus.FoodServed)
            {
                if (time > 3f)
                {
                    time = 0f;
                    tableStatus = TableStatus.PaymentRequested;
                }

                time += Time.deltaTime;
            }
        }

        public void SetTableStatus(TableStatus status)
        {
            tableStatus = status;
        }

        public void SetHasStaff(bool value)
        {
            hasStaff = value;
        }
    }
}
