using System.Collections.Generic;
using System.Linq;
using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffHolder : MyMonobehaviour
    {
        public static StaffHolder Instance { get; private set; }
        
        [SerializeField] private List<StaffBehaviour> staffs = new();

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
                        if (child.TryGetComponent(out StaffBehaviour newStaff))
                        {
                            staffs.Add(newStaff);
                        }
                    }
                }
            }
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            // LoadStaffs();
        }

        private void Update()
        {
            SetTableToFreeStaff(TableManager.Instance.GetTableWithPendingOrder());
            SetTableToFreeStaff(TableManager.Instance.GetTablePaymentRequested());
        }

        private void SetTableToFreeStaff(Table table)
        {
            if(table == null) return;
            
            StaffBehaviour staff = GetFreeStaff();
            if(staff == null) return;
            
            staff.targetTable = table;
            staff.targetTransform = table.StaffPoint;
            table.SetHasStaff(true);
        }

        private StaffBehaviour GetFreeStaff()
        {
            if (staffs.Count == 0) return null;
            
            return staffs.FirstOrDefault(s => s.IsFreeStaff());
        }

        public StaffBehaviour GetNewStaff()
        {
            if (transform.childCount == 0) return null;
            
            foreach (Transform child in transform)
            {
                if (!child.gameObject.activeSelf)
                {
                    child.gameObject.SetActive(true);
                    if (child.TryGetComponent(out StaffBehaviour newStaff))
                    {
                        staffs.Add(newStaff);
                        return newStaff;
                    }
                }
            }

            return null;
        }

        private void LoadStaffs()
        {
            if(staffs.Count > 0 || transform.childCount == 0) return;
            
            staffs.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out StaffBehaviour staff)) continue;
                if (child.gameObject.activeSelf)
                {
                    staffs.Add(staff);
                }
            }
            
            Debug.LogWarning(transform.name + ": LoadStaffs", gameObject);
        }
    }
}
