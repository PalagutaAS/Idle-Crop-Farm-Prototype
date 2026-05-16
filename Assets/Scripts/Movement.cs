using Player;
using Player.Input;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private Animator _animator;
    
    private IMovable _personController;
    private IInputService _inputService;
    
    private void Awake()
    {
        _inputService = new StandaloneInputService();
        _personController = GetComponent<IMovable>();
    }

    private void Update()
    {
        _animator.SetBool("IsMove", _inputService.AnyAxis);
        Vector3 moveVelocity = new Vector3(_inputService.Axis.x, 0f, _inputService.Axis.y);
        
        moveVelocity *= (_speed * Time.deltaTime);
        moveVelocity.y = (!_personController.IsGrounded) ? Physics.gravity.y * Time.deltaTime : 0;

        Vector3 movement = Vector3.ClampMagnitude(moveVelocity, _speed);
        
        _personController.Move(movement);
    }
}
