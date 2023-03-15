using System.Collections;
using UnityEngine;

public class FlyingCoin : MonoBehaviour
{
    [SerializeField] private AnimationCurve _moveCurve;

    public void MoveTo(Vector3 targetPosition)
    {
        StartCoroutine(MoveToPoint(transform.position, targetPosition));
    }
    
    private IEnumerator MoveToPoint(Vector3 startPosition, Vector3 targetPosition)
    {
        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            float x = Mathf.Lerp(startPosition.x, targetPosition.x, t);

            float yInterpolant = _moveCurve.Evaluate(t);
            float y = Mathf.LerpUnclamped(startPosition.y, targetPosition.y, yInterpolant);

            Vector3 position = new Vector3(x, y, 0f);
            transform.position = position;

            yield return null;
        }
        Destroy(gameObject);
    }
}
