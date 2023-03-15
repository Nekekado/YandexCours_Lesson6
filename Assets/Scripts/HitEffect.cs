using TMPro;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Animator _animator;

    public void Init(int value)
    {
        float durationAnimation = _animator.runtimeAnimatorController.animationClips[0].length;
        _text.text = "+" + value.ToString();
        Destroy(gameObject, durationAnimation);
    }
}
