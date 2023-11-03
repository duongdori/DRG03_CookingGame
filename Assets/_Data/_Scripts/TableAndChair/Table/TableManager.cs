using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TableAndChair
{
    public class TableManager : MyMonobehaviour
    {
        public static TableManager Instance { get; private set; }
        
        [SerializeField] private List<Table> tables = new();

        protected override void Awake()
        {
            base.Awake();
            if (Instance == null) Instance = this;
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadTables();
        }

        public Table GetEmptyTable()
        {
            if(tables.Count == 0) return null;
            return tables.FirstOrDefault(t => t.IsEmptyTable());
        }

        public Table GetTableWithPendingOrder()
        {
            if (tables.Count <= 0) return null;
            return tables.FirstOrDefault(t => t.IsPendingToOrder());
        }

        public Table GetTablePaymentRequested()
        {
            if (tables.Count <= 0) return null;
            return tables.FirstOrDefault(t => t.IsPaymentRequested());
        }

        private void LoadTables()
        {
            if(tables.Count > 0) return;
            if(transform.childCount <= 0) return;
            tables.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out Table table)) continue;
                tables.Add(table);
            }
            Debug.LogWarning(transform.name + ": LoadTables", gameObject);
        }
    }
}
