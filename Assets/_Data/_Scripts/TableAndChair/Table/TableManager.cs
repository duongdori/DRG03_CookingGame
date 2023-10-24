using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TableAndChair
{
    public class TableManager : MonoBehaviour
    {
        public static TableManager Instance { get; private set; }
        
        [SerializeField] private List<Table> tables = new();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            
            if(transform.childCount <= 0) return;
            tables.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out Table table)) continue;
                tables.Add(table);
            }
        }

        public Table GetEmptyTable()
        {
            if(tables.Count <= 0) return null;
            return tables.FirstOrDefault(t => t.TableStatus == TableStatus.Empty);
        }

        public Table GetTableWithPendingOrder()
        {
            if (tables.Count <= 0) return null;
            return tables.FirstOrDefault(t => t.TableStatus == TableStatus.WaitingForOrder && t.hasStaff == false);
        }

        public Table GetTablePaymentRequested()
        {
            if (tables.Count <= 0) return null;
            return tables.FirstOrDefault(t => t.TableStatus == TableStatus.PaymentRequested && t.hasStaff == false);
        }
    }
}
