using System;
using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    // [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private TextMeshPro textMesh;

    private void Awake()
    {
        // backgroundSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMeshPro>();
    }

    public void Setup(string text)
    {
        if (transform.parent.localScale.x == -1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        textMesh.SetText(text);
    }
}
