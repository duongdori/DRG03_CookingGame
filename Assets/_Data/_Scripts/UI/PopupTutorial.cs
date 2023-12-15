using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupTutorial : MonoBehaviour
{
    public TextMeshProUGUI textTutorial;
    public Button buttonNext;
    public int count = 0;
    private void Awake()
    {
        buttonNext.onClick.AddListener(OnButtonNext);
    }

    public void SetupText(string text)
    {
        textTutorial.text = text;
    }

    private void OnButtonNext()
    {
        if (count == 0)
        {
            MainUIManager.Instance.staffCategoryUI.gameObject.SetActive(true);
            MainUIManager.Instance.popupTutorial = this;
            gameObject.SetActive(false);
        }
        else
        {
            MainUIManager.Instance.shopCategoryUI.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
