using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TableAndChair
{
    public class TableManager : MyMonobehaviour
    {
        public static TableManager Instance { get; private set; }

        [SerializeField] private AstarPath aStarPath;
        [SerializeField] private List<Table> tables = new();

        protected override void Awake()
        {
            base.Awake();
            if (Instance == null) Instance = this;
            
            if (ES3.FileExists(ES3Settings.defaultSettings))
            {
                foreach (Transform child in transform)
                {
                    if (ES3.KeyExists(child.name))
                    {
                        child.gameObject.SetActive(true);
                        if (child.TryGetComponent(out Table table))
                        {
                            tables.Add(table);
                            aStarPath.Scan();
                        }
                    }
                }
            }
        }

        protected override void Start()
        {
            base.Start();
            aStarPath.Scan();
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            // LoadTables();
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
        
        public void GetNewTable(Table table, Sprite tableSprite, Sprite rightChair, Sprite leftChair)
        {
            if (transform.childCount == 0) return;
            
            if (!tables.Contains(table))
            {
                table.gameObject.SetActive(true);
                tables.Add(table);
                aStarPath.Scan();
            }
            
            table.SetupTable(tableSprite, rightChair, leftChair);
        }

        private void LoadTables()
        {
            if(tables.Count > 0) return;
            if(transform.childCount == 0) return;
            tables.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out Table table)) continue;
                if (child.gameObject.activeSelf)
                {
                    tables.Add(table);
                }
            }
            Debug.LogWarning(transform.name + ": LoadTables", gameObject);
        }
    }
}
