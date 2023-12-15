using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    public TextMeshProUGUI levelText;
    public Button continueButton;
    public Image iconNewFood;
    public GameObject rewards;

    public Sprite food1;
    public Sprite food2;
    public GameObject itemMoney1;
    public GameObject itemMoney2;
    public GameObject itemFood;
    private void OnEnable()
    {
        OnLevelChanged(GameManager.Instance.currentLevel);
        continueButton.onClick.AddListener((() =>
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }));
        
        levelText.text = GameManager.Instance.currentLevel.ToString();
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        GameManager.OnLevelChanged -= OnLevelChanged;
    }

    private void OnLevelChanged(int level)
    {
        if (level == 2)
        {
            itemMoney2.SetActive(false);
            iconNewFood.sprite = food1;
        }
        else if (level == 3)
        {
            itemMoney2.SetActive(false);
            iconNewFood.sprite = food2;
        }
        else
        {
            itemFood.SetActive(false);
            itemMoney1.SetActive(false);
            itemMoney2.SetActive(true);
        }
    }
}
