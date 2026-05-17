using Player;
using UnityEngine;
using VContainer;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private Animator _animator;
    
    private IMovable _personController;
    
    [Inject]
    private IInputService _inputService;
    
    private void Awake()
    {
        _personController = GetComponent<IMovable>();
    }

    private void Update()
    {
        _animator.speed = _inputService.AnyAxis ? _animator.speed : 1;
        _animator.SetBool("IsMove", _inputService.AnyAxis);
        if (!_inputService.AnyAxis)
            return;
        
        Vector3 moveVelocity = new Vector3(_inputService.Axis.x, 0f, _inputService.Axis.y);
        
        SmoothAnim(moveVelocity);

        moveVelocity *= (_speed * Time.deltaTime);
        moveVelocity.y = (!_personController.IsGrounded) ? Physics.gravity.y * Time.deltaTime : 0;

        Vector3 movement = Vector3.ClampMagnitude(moveVelocity, _speed);
        
        _personController.Move(movement);
    }

    private void SmoothAnim(Vector3 moveVelocity)
    {
        Vector3 desiredVelocity = moveVelocity * _speed;
        float normalizedSpeed = desiredVelocity.magnitude / _speed;
        _animator.speed = Mathf.Lerp(0.8f, 1f, normalizedSpeed);
    }
}
