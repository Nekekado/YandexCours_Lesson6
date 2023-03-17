using System;
using System.Collections;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public int Coins { get; private set; }
    public event Action<int> OnChangeCoins;
    public event Action<Vector3> OnCollectCoins;
    
    [SerializeField] private UICounter _counter;

    private void Start()
    {
        OnChangeCoins?.Invoke(Coins);
    }

    public void CollectCoins(int value, Vector3 worldPosition) 
    {
        OnCollectCoins?.Invoke(worldPosition);
        StartCoroutine(AddCoinsAfterDelay(value, 1f));
    }

    private IEnumerator AddCoinsAfterDelay(int value, float delay) 
    {
        yield return new WaitForSeconds(delay);
        Coins += value;
        OnChangeCoins?.Invoke(Coins);
        _counter.Display();
    }

    public bool TryBuy(int price) 
    {
        if (Coins >= price)
        {
            Coins -= price;
            _counter.Display();
            OnChangeCoins?.Invoke(Coins);
            return true;
        }
        
        return false;
    }
}
