using TMPro;
using UnityEngine;

public class NameBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private GameObject _canvas;

    public void ChangeName(string name)
    {
        if (_canvas.activeSelf == false)
        {
            _canvas.SetActive(true);
        }

        _name.text = name;
    }
}
