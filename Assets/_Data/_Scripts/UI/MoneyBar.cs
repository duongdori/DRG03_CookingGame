using TMPro;
using UnityEngine;

public class MoneyBar : MyMonobehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    protected override void Awake()
    {
        base.Awake();
        GameManager.Instance.OnMoneyChanged += SetTextValue;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadTextMesh();
    }

    private void SetTextValue(int value)
    {
        textMesh.SetText(value.ToString());
    }

    private void LoadTextMesh()
    {
        if(textMesh != null) return;
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        Debug.LogWarning(transform.name + ": LoadTextMesh", gameObject);
    }
}
