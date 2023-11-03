using System.Collections.Generic;
using System.Linq;
using Kitchen;
using UnityEngine;

namespace AssistantChefs
{
    public class AssistantChefHolder : MyMonobehaviour
    {
        [SerializeField] private List<AssistantChefBehaviour> assistantChefs = new();

        protected override void LoadComponents()
        {
            base.LoadComponents();
            LoadAssistantChefs();
        }

        private void Update()
        {
            FoodTray foodTray = KitchenManager.Instance.GetFoodTray();
            if(foodTray == null) return;

            AssistantChefBehaviour assistantChef = GetFreeAssistantChef();
            if(assistantChef == null) return;
            
            KitchenManager.Instance.RemoveFoodTray();
            assistantChef.foodTray = foodTray;
            assistantChef.targetTable = foodTray.targetTable;
            assistantChef.targetTransform = assistantChef.targetTable.assistantChefPoint;
        }

        private AssistantChefBehaviour GetFreeAssistantChef()
        {
            if (assistantChefs.Count == 0) return null;
            return assistantChefs.FirstOrDefault(a => a.IsFreeAssistantChef());
        }

        private void LoadAssistantChefs()
        {
            if(assistantChefs.Count > 0 || transform.childCount <= 0) return;
            
            assistantChefs.Clear();
            foreach (Transform child in transform)
            {
                if (!child.TryGetComponent(out AssistantChefBehaviour assistantChef)) continue;
                assistantChefs.Add(assistantChef);
            }
        }
    }
}