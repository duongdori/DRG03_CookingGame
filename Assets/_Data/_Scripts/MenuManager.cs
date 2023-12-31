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

        if (ES3.FileExists(ES3Settings.defaultSettings.path))
        {
            menu = ES3.Load("MenuFoodList", menu);
        }
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

    public void Save()
    {
        ES3.Save("MenuFoodList", menu);
    }
}
