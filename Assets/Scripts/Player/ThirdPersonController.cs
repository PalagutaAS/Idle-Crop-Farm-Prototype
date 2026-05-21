using Inventor;
using Player.Interface;
using Tools.Interface;
using UnityEngine;
using VContainer;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class ThirdPersonController : MonoBehaviour, IMovable, IPlayer
    {
        private IToolManager _toolManager;
        private CharacterController _characterController;
        public IInventoryChanger Inventory { get; private set; }
        public IWallet Wallet { get; private set; }
        public bool IsGrounded => _characterController.isGrounded;
        public Transform Transform => transform;
        public IToolManager Tools => _toolManager;

        [Inject]
        private void Constructor(IInventoryChanger inventory, IWallet wallet)
        {
            Inventory = inventory;
            Wallet = wallet;
            _characterController = GetComponent<CharacterController>();
            _toolManager = GetComponentInChildren<IToolManager>();
        }

        public void Move(Vector3 movement)
        {
            _characterController.Move(movement);
        }
        
        [ContextMenu("Payout + 5000")]
        private void Payout()
        {
            Wallet.Payout(5000);
        }
    }

    public interface IMovable
    {
        bool IsGrounded { get; }
        void Move(Vector3 movement);
    }
}