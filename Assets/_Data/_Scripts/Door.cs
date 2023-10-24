using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static Door Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
