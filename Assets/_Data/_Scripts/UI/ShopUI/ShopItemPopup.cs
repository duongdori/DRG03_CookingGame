using System;
using TableAndChair;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemPopup : MonoBehaviour
{
    [SerializeField] private ShopCategoryUI shopCategoryUI;
    [SerializeField] private ShopItemUI shopItemUI;

    [SerializeField] private Table targetTable;
    [SerializeField] private TableData tableData;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button popupButton;
    public bool isChoose;

    private void Awake()
    {
        shopCategoryUI = GetComponentInParent<ShopCategoryUI>();
        shopItemUI = GetComponentInParent<ShopItemUI>();
        Load();
    }

    private void Start()
    {
        popupButton.onClick.AddListener(BuyNewTable);
        Setup();
        UpdateShopItemButton();
        UpdatePopup();
    }

    public void Setup()
    {
        if(tableData == null) return;
        icon.sprite = tableData.icon;
        if (costText.gameObject.activeSelf)
        {
            costText.SetText(tableData.cost.ToString());
        }
    }
    
    public void UpdateShopItemButton()
    {
        popupButton.interactable = !isChoose;
        costText.gameObject.SetActive(!shopItemUI.isBuy);
    }

    public void UpdatePopup()
    {
        if (!shopItemUI.isBuy)
        {
            popupButton.interactable = GameManager.Instance.Money >= tableData.cost;
        }
        else
        {
            popupButton.interactable = !isChoose;
        }
    }
        

    private void BuyNewTable()
    {
        if (!shopItemUI.isBuy)
        {
            if(GameManager.Instance.Money < tableData.cost) return;
            GameManager.Instance.DeductMoney(tableData.cost);
        }
        
        TableManager.Instance.GetNewTable(targetTable, tableData.tableSprite, tableData.rightChairSprite,
            tableData.leftChairSprite);
        
        shopItemUI.item = this;
        isChoose = true;
        shopItemUI.isBuy = true;
        shopItemUI.UpdateStatus();
        //shopCategoryUI.UpdatePopup();
        shopCategoryUI.gameObject.SetActive(false);
    }

    public void SetIsChoose(bool value)
    {
        isChoose = value;
    }
    public void SetCostText(bool value)
    {
        costText.gameObject.SetActive(value);
    }
    
    public void Save()
    {
        ES3.Save("ShopItem_" + tableData.name + "_isChoose", isChoose);
    }
    
    public void Load()
    {
        if (ES3.FileExists(ES3Settings.defaultSettings.path))
        {
            isChoose = ES3.Load("ShopItem_" + tableData.name + "_isChoose", isChoose);
        }
        else
        {
            isChoose = false;
        }
        
        //UpdateShopItemButton();
    }
}
