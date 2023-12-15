using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SettingLanguageUI : MyMonobehaviour
{
    [SerializeField] private List<LanguageButtonUI> languageButtonUis;

    public ButtonSelectLanguage buttonSelectLanguage;

    protected override void Awake()
    {
        base.Awake();
    }

    private void OnEnable()
    {
        LanguageManager.OnLanguageChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged(Language language)
    {
        LanguageButtonUI languageButtonUI = languageButtonUis.FirstOrDefault(l => l.language == language);
        if(languageButtonUI == null) return;
        languageButtonUI.UpdateLanguageButton();
        gameObject.SetActive(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(languageButtonUis.Count > 0) return;
        languageButtonUis = GetComponentsInChildren<LanguageButtonUI>().ToList();
    }

    public void ResetIconCheck()
    {
        if(languageButtonUis.Count == 0) return;
        foreach (var languageButton in languageButtonUis) 
        {
            languageButton.SetIconCheck(false);
        }
    }
}
