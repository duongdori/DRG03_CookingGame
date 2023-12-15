using UnityEngine;
using UnityEngine.UI;

public class ButtonUI : MyMonobehaviour
{
    [SerializeField] private Button button;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(button != null) return;
        button = GetComponent<Button>();
    }

    protected override void Start()
    {
        base.Start();
        button.onClick.AddListener(() => SoundManager.Instance.PlaySfx(Sound.ButtonSfx));
    }
}
