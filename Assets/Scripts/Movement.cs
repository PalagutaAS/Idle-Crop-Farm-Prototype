using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    [SerializeField] private Animator _animator;
    
    private IMovable _personController;
    private IInput _input;
    
    private void Awake()
    {
        _input = GetComponent<IInput>();
        _personController = GetComponent<IMovable>();
    }

    private void Update()
    {
        _animator.SetBool("IsMove", _input.GetKey);
        Vector3 moveVelocity = new Vector3(_input.Horizontal, 0f, _input.Vertical);
        
        moveVelocity *= (_speed * Time.deltaTime);
        moveVelocity.y = (!_personController.IsGrounded) ? Physics.gravity.y * Time.deltaTime : 0;

        Vector3 movement = Vector3.ClampMagnitude(moveVelocity, _speed);
        
        _personController.Move(movement);
    }
}
