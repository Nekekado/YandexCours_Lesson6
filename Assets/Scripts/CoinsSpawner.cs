using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _forms;
    [SerializeField] private float _scale = 0.25f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _targetsRadius;
    

    private GameObject _activeForm;
    private List<Vector3> _coinsPositions = new List<Vector3>();
    private float _minTargetsRadius;
    private float _maxTargetsRadius;


    private void Start()
    {
        _activeForm = TakeActiveForm();

        _minTargetsRadius = _activeForm.transform.localScale.y / 2f;
        _maxTargetsRadius = _targetsRadius;
    }
    
    public void SpawnCoin()
    {
        _activeForm = TakeActiveForm();
        
        Vector3 targetPosition = TakeRandomPosition();
        
        _coinsPositions.Add(targetPosition);
        Vector3 spawnPosition = _spawnPosition.position;
        spawnPosition.y -= _scale / 2;

        GameObject coin = CreateCoin();

        StartCoroutine(JumpCorutine(spawnPosition, targetPosition, coin));
    }

    private GameObject CreateCoin()
    {
        GameObject coin = Instantiate(_activeForm, _activeForm.transform.position, Quaternion.identity);
        Hover hover = coin.AddComponent<Hover>();
        Clickable clickable = _activeForm.GetComponentInParent<Clickable>();
        hover.SetClickable(clickable);
        coin.transform.localScale = Vector3.one * _scale;

        return coin;
    }

    private Vector3 TakeRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        
        while (true)
        {
            float x = Random.Range(-_targetsRadius, _targetsRadius);
            float z = Random.Range(-_targetsRadius, _targetsRadius);
            
            bool inSpawnRadius = x * x + z * z <= _maxTargetsRadius * _maxTargetsRadius &&
                                 x * x + z * z > _minTargetsRadius * _minTargetsRadius;
            
            bool inTargetRadius = false;

            foreach (var variable in _coinsPositions)
            { 
                inTargetRadius =
                    Mathf.Pow(x - variable.x, 2) + Mathf.Pow(z - variable.z, 2) <= _scale * _scale;
                    
                if(inTargetRadius) break;
            }
            
            if (inSpawnRadius && inTargetRadius == false)
            {
                randomPosition = new Vector3(x, _scale / 2, z);
                break;
            }
        }

        return randomPosition;
    }
    
    private IEnumerator JumpCorutine(Vector3 startPosition, Vector3 targetPosition, GameObject coin)
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            float yInterpolant = _jumpCurve.Evaluate(t);
            float y = Mathf.Lerp(startPosition.y + yInterpolant, targetPosition.y, t);
            float x = Mathf.Lerp(startPosition.x, targetPosition.x, t);
            float z = Mathf.Lerp(startPosition.z, targetPosition.z, t);
            Vector3 nextPosition = new Vector3(x, y, z);

            coin.transform.position = nextPosition;
            
            yield return null;
        }

        coin.AddComponent<BoxCollider>();
    }

    private GameObject TakeActiveForm()
    {
        GameObject activeForm = _forms[0];

        foreach (var x in _forms)
        {
            if (x.activeSelf)
            {
                activeForm = x;
                break;
            }
        }

        return activeForm;
    }
}
