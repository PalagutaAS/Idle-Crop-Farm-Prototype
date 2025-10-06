using Inventor;
using Player.Interface;
using Tools;
using Tools.Interface;
using UnityEngine;
using VContainer;
using Wallets;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class ThirdPersonController : MonoBehaviour, IMovable, IPlayer
    {
        private IToolManager _toolManager;
        private CharacterController _characterController;
        private Inventory _inventory;
        public Inventory Inventory => _inventory;
        public IWallet Wallet { get; private set; }
        public bool IsGrounded => _characterController.isGrounded;
        public Transform Transform => transform;
        public IToolManager Tools => _toolManager;

        [Inject]
        private void Constructor(Inventory inventory, IWallet wallet)
        {
            _inventory = inventory;
            Wallet = wallet;
            _characterController = GetComponent<CharacterController>();
            _toolManager = GetComponentInChildren<IToolManager>();
        }

        private void Awake()
        {
            _toolManager.TrySetupNewTool(ToolType.Pickaxe);
        }

        public void Move(Vector3 movement)
        {
            _characterController.Move(movement);
        }
        
        [ContextMenu("Payout + 500")]
        private void Payout()
        {
            Wallet.Payout(500);
        }
    }

    public interface IMovable
    {
        bool IsGrounded { get; }
        void Move(Vector3 movement);
    }
}