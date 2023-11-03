using System.Collections.Generic;
using UnityEngine;

public class FoodHolder : MyMonobehaviour
{
    public List<Food> holder = new();

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadHolder();
    }

    private void LoadHolder()
    {
        if(holder.Count > 0 || transform.childCount == 0) return;
        holder.Clear();
        
        foreach (Transform child in transform)
        {
            if(!child.TryGetComponent(out Food food)) continue;
            holder.Add(food);
        }
        Debug.LogWarning(transform.name + ": LoadHolder", gameObject);
    }
}
