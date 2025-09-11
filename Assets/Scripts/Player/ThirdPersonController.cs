using UnityEngine;
using Wallets;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour, IMovable
{
    [SerializeField] private Tool _tool;
    
    private CharacterController _characterController;
    public bool IsGrounded => _characterController.isGrounded;
    
    private Inventory _inventory;
    private Wallet _wallet;
    public Inventory Inventory => _inventory;
    public Wallet Wallet => _wallet;
    public Tool Tool => _tool;
    
    private void Awake()
    {
        _wallet = new Wallet();
        _inventory = new Inventory();
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector3 movement)
    {
        _characterController.Move(movement);
    }
}

public interface IMovable
{
    bool IsGrounded { get; }
    void Move(Vector3 movement);
}
