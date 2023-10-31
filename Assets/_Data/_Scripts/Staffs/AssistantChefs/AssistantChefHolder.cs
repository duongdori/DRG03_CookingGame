using System;
using System.Collections.Generic;
using System.Linq;
using Kitchen;
using UnityEngine;

namespace AssistantChefs
{
    public class AssistantChefHolder : MonoBehaviour
    {
        [SerializeField] private List<AssistantChefBehaviour> assistantChefs = new();
        
        private void Awake()
        {
            if(transform.childCount <= 0) return;
            assistantChefs.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out AssistantChefBehaviour assistantChef)) continue;
                assistantChefs.Add(assistantChef);
            }
        }

        private void Update()
        {
            FoodTray foodTray = KitchenManager.Instance.GetFoodTray();
            if(foodTray == null) return;

            AssistantChefBehaviour assistantChef = GetFreeAssistantChef();
            if(assistantChef == null) return;
            
            KitchenManager.Instance.RemoveFoodTray();
            assistantChef.foodTray = foodTray;
            foodTray.transform.SetParent(assistantChef.transform);
            assistantChef.targetTable = foodTray.targetTable;
            assistantChef.targetTransform = assistantChef.targetTable.assistantChefPoint;
            foodTray.gameObject.SetActive(false);
        }

        private AssistantChefBehaviour GetFreeAssistantChef()
        {
            if (assistantChefs.Count == 0) return null;
            return assistantChefs.FirstOrDefault(a => a.StateMachine.CurrentState == a.IdleState && a.isFree);
        }
    }
}