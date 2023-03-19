using System.Collections;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private float _scaleTime = 0.25f;
    [SerializeField] private HitEffect _hitEffectPrefab;
    [SerializeField] private Resources _resources;
    [SerializeField] private GameObject _testWindow;
    [SerializeField] private CoinsSpawner _coinsSpawner;

    private int _coinsPerClick = 1;

    private void OnMouseDown()
    {
        if (_testWindow.activeSelf == false)
        {
            Hit();
        }
    }

    public void Hit()
    {
        _coinsSpawner.SpawnCoin();
        StartCoroutine(HitAnimation());
    }

    public void Collect(Vector3 collectPosition)
    {
        HitEffect hitEffect = Instantiate(_hitEffectPrefab, transform.position, Quaternion.identity);
        hitEffect.Init(_coinsPerClick);
        _resources.CollectCoins(_coinsPerClick, collectPosition);
    }
    
    private IEnumerator HitAnimation()
    {
        for (float t = 0; t < 1f; t += Time.deltaTime / _scaleTime)
        {
            float scale = _scaleCurve.Evaluate(t);
            transform.localScale = Vector3.one * scale;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }
    
    public void AddCoinsPerClick(int value)
    {
        _coinsPerClick += value;
    }
}
