using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private int money;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddMoney(int value)
    {
        money += value;
    }
}
