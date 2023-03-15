using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _forms;
    [SerializeField] private List<Transform> _targetPositions;
    [SerializeField] private float _scale = 0.25f;
    [SerializeField] private AnimationCurve _jumpCurve;
    [SerializeField] private Transform _spawnPosition;

    private GameObject _activeForm;
    
    public void SpawnCoin()
    { 
        _activeForm = _forms[0];

        foreach (var x in _forms)
        {
            if (x.activeSelf)
            {
                _activeForm = x;
                break;
            }
        }

        int index = Random.Range(0, _targetPositions.Count);
        Vector3 targetPosition = _targetPositions[index].position;
        targetPosition.y = _scale / 2;
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
}
