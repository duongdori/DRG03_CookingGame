using UnityEngine;
using UnityEngine.UI;

public class LanguageButtonUI : MyMonobehaviour
{
    [SerializeField] private SettingLanguageUI settingLanguageUI;
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private GameObject iconCheck;

    public Language language;
    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener((Setup));
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(button != null) return;
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        iconCheck = transform.GetChild(0).gameObject;
        settingLanguageUI = GetComponentInParent<SettingLanguageUI>();
    }

    private void Setup()
    {
        UpdateLanguageButton();
        LanguageManager.Instance.SetLanguage(language);
    }

    public void UpdateLanguageButton()
    {
        settingLanguageUI.ResetIconCheck();
        SetIconCheck(true);
        settingLanguageUI.buttonSelectLanguage.Setup(image.sprite);
    }
    public void SetIconCheck(bool value)
    {
        iconCheck.SetActive(value);
    }
}