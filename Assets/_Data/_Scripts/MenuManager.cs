using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; private set; }
    
    [SerializeField] private List<FoodData> menu = new();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
    public FoodData GetRandomFoodFromMenu()
    {
        if (menu.Count <= 0) return null;
        int index = Random.Range(0, menu.Count);

        return menu[index];
    }
    public void AddToMenu(FoodData foodData)
    {
        if(menu.Contains(foodData)) return;
        menu.Add(foodData);
    }

    public void RemoveFromMenu(FoodData foodData)
    {
        if(!menu.Contains(foodData)) return;
        menu.Remove(foodData);
    }
}
