using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [SerializeField] private int money;

    public event UnityAction<int> OnMoneyChanged; 
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddMoney(int value)
    {
        money += value;
        OnMoneyChanged?.Invoke(money);
    }

    public void DeductMoney(int value)
    {
        money -= value;
        OnMoneyChanged?.Invoke(money);
    }
}
