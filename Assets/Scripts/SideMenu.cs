using UnityEngine;

public class SideMenu : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isActive = true;
    
    public void Switch() 
    {
        _isActive = !_isActive;

        if (_isActive) 
        {
            _animator.SetTrigger("Show");
        }
        else 
        {
            _animator.SetTrigger("Hide");
        }
    }
}
