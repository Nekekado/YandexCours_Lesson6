using System;
using UnityEngine;

public class WindowChangingName : MonoBehaviour
{
    [SerializeField] private NameBar _nameBar;

    public void StartChangeName(string name)
    {
        if(string.IsNullOrEmpty(name)) return;
        
        _nameBar.ChangeName(name);
    }
}
