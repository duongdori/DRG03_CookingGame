using System;
using Assets.SimpleLocalization.Scripts;
using UnityEngine;
using UnityEngine.Events;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }

    public static event UnityAction<Language> OnLanguageChanged;

    private const string SaveLanguage = "SaveLanguage.es3";
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
        
        LocalizationManager.Read();
    }

    private void Start()
    {
        if (ES3.FileExists(SaveLanguage))
        {
            LocalizationManager.Language = ES3.Load<string>("currentLanguage", SaveLanguage);
            OnLanguageChanged?.Invoke(Enum.Parse<Language>(LocalizationManager.Language));
        }
        else
        {
            SetLanguage(Language.Vietnamese);
            OnLanguageChanged?.Invoke(Enum.Parse<Language>(LocalizationManager.Language));
        }
    }
    
    public void SetLanguage(Language language)
    {
        LocalizationManager.Language = language.ToString();
    }

    private void OnApplicationQuit()
    {
        ES3.Save("currentLanguage", LocalizationManager.Language, SaveLanguage);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            ES3.Save("currentLanguage", LocalizationManager.Language, SaveLanguage);
        }
    }
}


public enum Language
{
    English,
    Vietnamese,
    Korean,
}