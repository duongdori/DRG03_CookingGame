using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MenuCategoryUI : MyMonobehaviour
{
    [SerializeField] private List<MenuItemUI> menuItems = new List<MenuItemUI>();

    protected override void Awake()
    {
        base.Awake();
        GameManager.OnLevelChanged += OnLevelChanged;
        GameManager.OnLevelSaveChanged += OnLevelChanged;
    }

    protected override void Start()
    {
        base.Start();
        gameObject.SetActive(false);
    }

    private void OnLevelChanged(int level)
    {
        if (level == 2)
        {
            menuItems[5].SetupFood(true);
            MenuManager.Instance.AddToMenu(menuItems[5].GetFoodData());
        }
        else if (level == 3)
        {
            menuItems[6].SetupFood(true);
            MenuManager.Instance.AddToMenu(menuItems[6].GetFoodData());
        }
        else
        {
            if (level != 1)
            {
                menuItems[5].SetupFood(true);
                menuItems[6].SetupFood(true);
            }
        }
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(menuItems.Count > 0) return;
        menuItems = GetComponentsInChildren<MenuItemUI>().ToList();
    }

    private void OnEnable()
    {
        OnLevelChanged(GameManager.Instance.currentLevel);
        
        foreach (var item in menuItems)
        {
            item.Setup();
        }
    }

    private void OnDestroy()
    {
        GameManager.OnLevelChanged -= OnLevelChanged;
        GameManager.OnLevelSaveChanged -= OnLevelChanged;
    }
}
