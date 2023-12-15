using System.Collections.Generic;
using System.Linq;
using Assets.SimpleLocalization.Scripts;
using TableAndChair;
using TMPro;
using UnityEngine;

public class ShopItemUI : MonoBehaviour
{
    public TableData tableData;
    public TextMeshProUGUI tableNameText;
    public bool isBuy;

    public ShopItemPopup item;
    public List<ShopItemPopup> shopItemPopups = new List<ShopItemPopup>();
    
    private void Awake()
    {
        Load();
        if(shopItemPopups.Count > 0) return;
        shopItemPopups = GetComponentsInChildren<ShopItemPopup>().ToList();
    }

    private void Start()
    {
        UpdateTableNameText();
    }

    public void UpdateStatus()
    {
        foreach (var itemPopup in shopItemPopups)
        {
            if (itemPopup != item)
            {
                itemPopup.SetIsChoose(false);
            }
            itemPopup.SetCostText(false);
            itemPopup.UpdateShopItemButton();
        }
    }
    

    public void UpdateTableNameText()
    {
        var text = LocalizationManager.Localize("Shop.Table", tableData.tableName);
        tableNameText.SetText(text);
    }
    
    public void Save()
    {
        ES3.Save("Table" + tableData.tableName + "_isBuy", isBuy);
    }
    
    public void Load()
    {
        if (ES3.FileExists(ES3Settings.defaultSettings.path))
        {
            isBuy = ES3.Load<bool>("Table" + tableData.tableName + "_isBuy");
        }
        else
        {
            isBuy = false;
        }
        
        // foreach (var itemPopup in shopItemPopups)
        // {
        //     itemPopup.Load();
        // }
    }
}
