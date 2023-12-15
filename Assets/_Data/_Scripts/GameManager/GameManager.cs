using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private int money;
    public int Money => money;

    public int currentLevel;
    public float currentExperience;
    public float maxExperience;

    public event UnityAction<int> OnMoneyChanged;
    public static event UnityAction<int> OnLevelChanged;
    public static event UnityAction<int> OnLevelSaveChanged;
    public static event UnityAction<float, float> OnExperienceChanged; 
    private void Awake()
    {
        if (Instance == null) Instance = this;
        SceneManager.sceneUnloaded += scene => { Save(); };
    }

    private void Start()
    {
        if (ES3.FileExists(ES3Settings.defaultSettings.path))
        {
            money = ES3.Load<int>("money");
            OnMoneyChanged?.Invoke(money);

            int level = ES3.Load<int>("currentLevel");
            SetLevel(level);
            
            float experience = ES3.Load<float>("currentExperience");
            SetCurrentExperience(experience);
            
            maxExperience = ES3.Load<float>("maxExperience");
        }
        else
        {
            currentLevel = 1;
            maxExperience = 100f;
            currentExperience = 0f;
            AddMoney(300);
        }
    }

    public void AddMoney(int value)
    {
        money += value;
        OnMoneyChanged?.Invoke(money);
    }

    public void DeductMoney(int value)
    {
        money -= value;
        OnMoneyChanged?.Invoke(money);
    }

    public void AddExperience(float value)
    {
        currentExperience += value;
        if (currentExperience >= maxExperience)
        {
            currentExperience = 0f;
            UpLevel();
        }
        OnExperienceChanged?.Invoke(currentExperience, maxExperience);
    }
    private void UpLevel()
    {
        currentLevel += 1;
        maxExperience += 40;
        AddMoney(200);
        OnLevelChanged?.Invoke(currentLevel);
    }

    private void SetLevel(int value)
    {
        currentLevel = value;
        OnLevelSaveChanged?.Invoke(currentLevel);
    }

    private void SetCurrentExperience(float value)
    {
        currentExperience = value;
        OnExperienceChanged?.Invoke(currentExperience, maxExperience);
    }

    private void Save()
    {
        ES3.Save("money", money);
        ES3.Save("currentLevel", currentLevel);
        ES3.Save("currentExperience", currentExperience);
        ES3.Save("maxExperience", maxExperience);
    }
    
    private void OnApplicationQuit()
    {
        Save();
        MenuManager.Instance.Save();
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            Save();
            MenuManager.Instance.Save();
        }
    }
}
