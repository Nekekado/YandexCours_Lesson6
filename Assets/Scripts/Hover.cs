using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] private Clickable _clickable;

    public void SetClickable(Clickable clickable)
    {
        _clickable = clickable;
    }
    private void OnMouseEnter()
    {
        _clickable.Collect();
        Destroy(gameObject);
    }
}
