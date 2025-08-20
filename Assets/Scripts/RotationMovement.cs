using UnityEngine;

public class RotationMovement : MonoBehaviour
{
    [SerializeField] private float _scaleRotation = 500f;
    
    private IInput _input;
    private void Awake()
    {
        _input = GetComponent<IInput>();
    }
    private void LateUpdate()
    {
        if (!_input.GetKey) return;

        RotateTowards(new Vector3(_input.Horizontal, 0f, _input.Vertical));
    }

    private void RotateTowards(Vector3 direction)
    {
        if (direction == Vector3.zero) return;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            _scaleRotation * Time.deltaTime
        );
    }
}
