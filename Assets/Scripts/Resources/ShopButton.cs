using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Resources _resources;
    [SerializeField] private int _price;
    [SerializeField] private Clickable _clickable;
    [SerializeField] private int _procentUpPrice;
    [SerializeField] private TMP_Text _buttonText;

    private void Start()
    {
        _button.onClick.AddListener(Buy);
        _buttonText.text = $"Upgrade click for:{_price}";
    }

    private void UpdateButtonState(int coins) 
    {
        _button.interactable = coins >= _price;
    }

    public void Buy() 
    {
        if (_resources.TryBuy(_price)) 
        {
            _clickable.AddCoinsPerClick(1);
            _price += (int)(_price / 100f * _procentUpPrice);
            _buttonText.text = $"Upgrade click for:{_price}";
        }
    }

    private void OnEnable()
    {
        _resources.OnChangeCoins += UpdateButtonState;
    }

    private void OnDisable()
    {
        _resources.OnChangeCoins -= UpdateButtonState;
    }
}
