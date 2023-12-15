using Assets.SimpleLocalization.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnterNameUI : MonoBehaviour
{
    public StatusBar statusBar;
    public TMP_InputField inputField;
    public Button buttonConfirm;
    public PopupTutorial popupTutorial;

    private void OnEnable()
    {
        statusBar = FindObjectOfType<StatusBar>().GetComponent<StatusBar>();
    }

    private void Start()
    {
        buttonConfirm.onClick.AddListener(OnButtonConfirmClick);
        inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
        OnInputFieldValueChanged(inputField.text);
    }

    private void OnInputFieldValueChanged(string newText)
    {
        buttonConfirm.interactable = !string.IsNullOrEmpty(newText);
    }

    private void OnButtonConfirmClick()
    {
        statusBar.nameText.text = inputField.text;
        ES3.Save("RestaurantName", inputField.text);
        gameObject.SetActive(false);
        popupTutorial.gameObject.SetActive(true);
        popupTutorial.SetupText(LocalizationManager.Localize("Introduce.Text3"));
    }
}
