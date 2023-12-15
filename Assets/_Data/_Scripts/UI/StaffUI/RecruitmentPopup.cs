using Assets.SimpleLocalization.Scripts;
using Staffs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentPopup : MonoBehaviour
{
    public StaffCategoryUI staffCategoryUI;
    
    public StaffData staffData;
    public Image icon;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI costText;
    public Button recruitButton;
    public TextMeshProUGUI recruitText;
    public bool isRecruit = false;

    private void Awake()
    {
        staffCategoryUI = GetComponentInParent<StaffCategoryUI>();
    }

    private void Start()
    {
        recruitButton.onClick.AddListener(Recruit);
        Setup();
        //recruitText.text = LocalizationManager.Localize("Staff.Recruit");
        UpdateRecruitButton();
    }

    public void Setup()
    {
        if(staffData == null) return;
        icon.sprite = staffData.icon;
        nameText.SetText(staffData.name);
        var text = LocalizationManager.Localize("Staff.Cost", staffData.cost);
        costText.SetText(text);
    }

    public void UpdateRecruitButton()
    {
        if (isRecruit)
        {
            recruitButton.interactable = false;
            recruitText.text = LocalizationManager.Localize("Staff.Recruited");
        }
        else
        {
            recruitText.text = LocalizationManager.Localize("Staff.Recruit");
            recruitButton.interactable = GameManager.Instance.Money >= staffData.cost;
        }
    }

    private void Recruit()
    {
        if(GameManager.Instance.Money < staffData.cost) return;
        GameManager.Instance.DeductMoney(staffData.cost);
        StaffHolder.Instance.GetNewStaff();
        isRecruit = true;
        staffCategoryUI.UpdatePopup();
        staffCategoryUI.gameObject.SetActive(false);
        MainUIManager.Instance.SetupPopupTutorial();
    }
    
    public void Save()
    {
        ES3.Save(staffData.name + "_isRecruit", isRecruit);
    }

    public void Load()
    {
        if (ES3.FileExists(ES3Settings.defaultSettings.path))
        {
            isRecruit = ES3.Load(nameText.text + "_isRecruit", isRecruit);
        }
        else
        {
            isRecruit = false;
        }
    }
}
