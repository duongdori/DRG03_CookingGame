using UnityEngine;

public class Food : MyMonobehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadSpriteRenderer();
        spriteRenderer.sprite = null;
    }

    public void Setup(Sprite sprite, string foodName)
    {
        transform.name = foodName;
        spriteRenderer.sprite = sprite;
    }

    private void LoadSpriteRenderer()
    {
        if(spriteRenderer != null) return;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.LogWarning(transform.name + ": LoadSpriteRenderer", gameObject);
    }
}
