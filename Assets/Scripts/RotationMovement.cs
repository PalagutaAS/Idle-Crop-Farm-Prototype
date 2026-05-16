using UnityEngine;

public class RotationMovement : MonoBehaviour
{
    [SerializeField] private float _scaleRotation = 500f;
    
    private IInputService _inputService;
    private void Awake()
    {
        _inputService = new StandaloneInputService();
    }
    private void LateUpdate()
    {
        if (!_inputService.AnyAxis) return;

        RotateTowards(new Vector3(_inputService.Axis.x, 0f, _inputService.Axis.y));
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
