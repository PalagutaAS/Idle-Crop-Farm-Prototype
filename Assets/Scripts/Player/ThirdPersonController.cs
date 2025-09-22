using Player.Interface;
using Player.Tools;
using UnityEngine;
using Wallets;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class ThirdPersonController : MonoBehaviour, IMovable, IPlayer
    {
        [SerializeField] private PlayerTools _playerTools;
        private CharacterController _characterController;
        
        private Inventory _inventory;
        private Wallet _wallet;
        public Inventory Inventory => _inventory;
        public Wallet Wallet => _wallet;
        public bool IsGrounded => _characterController.isGrounded;
        public Transform Transform => transform;
    
        private void Awake()
        {
            _wallet = new Wallet();
            _inventory = new Inventory();
            _characterController = GetComponent<CharacterController>();
        }

        private void Start()
        {
            _playerTools.Init(this);
            _playerTools.TrySetupNewTool();
        }
        
        public void Move(Vector3 movement)
        {
            _characterController.Move(movement);
        }

        public bool TryBuyTool()
        {
            return _playerTools.TrySetupNewTool();
        }
    }

    public interface IMovable
    {
        bool IsGrounded { get; }
        void Move(Vector3 movement);
    }
}