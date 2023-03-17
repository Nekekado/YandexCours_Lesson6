using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectsRotationCheck : MonoBehaviour
{
    [SerializeField] private List<Transform> _objects;
    [SerializeField] private Transform _camera;

    private void Update()
    {
        foreach (var x in _objects)
        {
            x.LookAt(_camera);
        }

        HitEffect[] _effects = FindObjectsByType<HitEffect>(sortMode:FindObjectsSortMode.None);
        
        foreach (var x in _effects)
        {
            x.gameObject.transform.LookAt(_camera);
        }
    }
}
