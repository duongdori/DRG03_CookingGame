using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectLanguage : MyMonobehaviour
{
    [SerializeField] private Image flagImage;
    [SerializeField] private TextMeshProUGUI nameText;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(flagImage != null && nameText != null) return;
        flagImage = transform.GetChild(1).GetComponentInChildren<Image>();
        nameText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Setup(Sprite flag)
    {
        flagImage.sprite = flag;
    }
}
