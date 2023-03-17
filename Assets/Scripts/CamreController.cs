using UnityEngine;

public class CamreController : MonoBehaviour
{
    [SerializeField] private float _speedRotation;
    [SerializeField] private Transform _target;

    private float _radius;
    private float _angelRotation;

    private void Start()
    {
        _angelRotation = transform.rotation.y;
        _radius = (transform.position - _target.position).magnitude;
        
        RotateCamera(true);
    }

    private void LateUpdate()
    {
        RotateCamera(false);
    }

    private void RotateCamera(bool inStart)
    {
        float buttonAxis = Input.GetAxisRaw("Horizontal");

        if (buttonAxis != 0 || inStart)
        {
            _angelRotation += _speedRotation * buttonAxis * Time.deltaTime;

            float x = _radius * Mathf.Cos(_angelRotation);
            float z = _radius * Mathf.Sin(_angelRotation);

            Vector3 positionRotation = new Vector3(x, transform.position.y, z);

            transform.position = positionRotation;
            transform.LookAt(_target);
        }
    }
}
