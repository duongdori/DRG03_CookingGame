using System;
using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupIntroduceUI : MonoBehaviour
{
    public EnterNameUI enterNameUI;

    public TextMeshProUGUI introduceText;
    public Button buttonNext;
    public TextMeshProUGUI textButtonNext;
    public Button buttonEnterName;
    public TextMeshProUGUI textEnterName;


    private void Awake()
    {
        buttonNext.onClick.AddListener(UpdateTextIntroduce);
        buttonEnterName.onClick.AddListener(OnButtonEnterNameClick);
    }

    private void OnEnable()
    {
        introduceText.text = LocalizationManager.Localize("Introduce.Text1");
    }

    private void UpdateTextIntroduce()
    {
        introduceText.text = LocalizationManager.Localize("Introduce.Text2");
        textButtonNext.gameObject.SetActive(false);
        buttonEnterName.gameObject.SetActive(true);
    }

    private void OnButtonEnterNameClick()
    {
        enterNameUI.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
