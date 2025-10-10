using Inventor;
using Tools.Interface;
using UnityEngine;

namespace Player.Interface
{
    public interface IPlayer
    {
        Transform Transform { get; }
        IInventoryChanger Inventory { get; }
        IWallet Wallet { get; }
        
        IToolManager Tools { get; }  
    }
}