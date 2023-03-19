using System;
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
        _clickable.Collect(transform.position);
        Destroy(gameObject);
    }
}
