using System;
using System.Collections.Generic;
using System.Linq;
using TableAndChair;
using UnityEngine;

namespace Staffs
{
    public class StaffHolder : MonoBehaviour
    {
        [SerializeField] private List<StaffBehaviour> staffs = new();

        private void Awake()
        {
            if(transform.childCount <= 0) return;
            staffs.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out StaffBehaviour staff)) continue;
                staffs.Add(staff);
            }
        }

        private void Update()
        {
            SetTablePendingOrderToStaff();
            
            SetTablePaymentRequestedToStaff();
        }

        private void SetTablePendingOrderToStaff()
        {
            Table table = TableManager.Instance.GetTableWithPendingOrder();
            if(table == null) return;

            StaffBehaviour staff = GetFreeStaff();
            if(staff == null) return;

            table.SetHasStaff(true);
            staff.targetTable = table;
            staff.targetTransform = table.StaffPoint;
        }

        private void SetTablePaymentRequestedToStaff()
        {
            Table table = TableManager.Instance.GetTablePaymentRequested();
            if(table == null) return;

            StaffBehaviour staff = GetFreeStaff();
            if(staff == null) return;
            
            table.SetHasStaff(true);
            staff.targetTable = table;
            staff.targetTransform = table.StaffPoint;
        }

        private StaffBehaviour GetFreeStaff()
        {
            if (staffs.Count == 0) return null;
            
            return staffs.FirstOrDefault(s => s.StateMachine.CurrentState == s.IdleState && s.isFree);
        }
    }
}
