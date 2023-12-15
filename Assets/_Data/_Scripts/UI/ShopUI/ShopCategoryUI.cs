using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopCategoryUI : MyMonobehaviour
{
    [SerializeField] private List<ShopItemPopup> shopItemPopups = new();
    [SerializeField] private List<ShopItemUI> shopItemUI = new();
    

    protected override void Awake()
    {
        base.Awake();
        UpdateShopCategory();
        //Load();
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(shopItemPopups.Count > 0) return;
        shopItemPopups = GetComponentsInChildren<ShopItemPopup>().ToList();
        shopItemUI = GetComponentsInChildren<ShopItemUI>().ToList();
    }

    private void OnEnable()
    {
        UpdatePopup();
    }

    public void UpdateShopCategory()
    {
        foreach (var popup in shopItemPopups)
        {
            popup.Setup();
        }
    }

    public void UpdateShopItemName()
    {
        foreach (var itemUI in shopItemUI)
        {
            itemUI.UpdateTableNameText();
        }
    }

    public void UpdatePopup()
    {
        foreach (var popup in shopItemPopups)
        {
            popup.UpdatePopup();
        }
    }
    
    public void Save()
    {
        foreach (var itemPopup in shopItemUI)
        {
            itemPopup.Save();
        }

        foreach (var itemPopup in shopItemPopups)
        {
            itemPopup.Save();
        }
    }

    public void Load()
    {
        foreach (var itemPopup in shopItemUI)
        {
            itemPopup.Load();
        }
    }
}
