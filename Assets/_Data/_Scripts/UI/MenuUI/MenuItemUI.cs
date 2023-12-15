using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuItemUI : MyMonobehaviour
{
    [SerializeField] private FoodData foodData;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI foodName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI price;
    public GameObject moneyIcon;
    public GameObject image;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(foodData == null) return;
        icon.sprite = foodData.sprite;
    }

    public void Setup()
    {
        price.text = LocalizationManager.Localize("Menu.Price", foodData.price);
    }

    public void SetupFood(bool value)
    {
        icon.gameObject.SetActive(value);
        foodName.gameObject.SetActive(value);
        description.gameObject.SetActive(value);
        moneyIcon.gameObject.SetActive(value);
        price.gameObject.SetActive(value);
        image.SetActive(!value);
    }

    public FoodData GetFoodData()
    {
        return foodData;
    }
}
