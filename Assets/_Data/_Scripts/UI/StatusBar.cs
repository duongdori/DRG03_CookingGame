using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;

    public LevelUpUI levelUpUI;
    
    private void Awake()
    {
        GameManager.OnLevelChanged += OnLevelChanged;
        GameManager.OnLevelSaveChanged += OnLevelSaveChanged;
        GameManager.OnExperienceChanged += OnExperienceChanged;
        
        if (ES3.FileExists(ES3Settings.defaultSettings))
        {
            nameText.text = ES3.Load<string>("RestaurantName");
        }
    }

    private void OnLevelSaveChanged(int level)
    {
        levelText.text = level.ToString();
    }

    private void OnLevelChanged(int level)
    {
        levelText.text = level.ToString();
        levelUpUI.gameObject.SetActive(true);
        SoundManager.Instance.PlaySfx(Sound.LevelUp);
    }

    private void OnExperienceChanged(float value, float maxValue)
    {
        slider.value = value / maxValue;
    }
}
