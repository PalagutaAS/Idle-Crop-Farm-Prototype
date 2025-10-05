using Inventor;
using Tools.Interface;
using UnityEngine;

namespace Player.Interface
{
    public interface IPlayer
    {
        Transform Transform { get; }
        Inventory Inventory { get; }
        IWallet Wallet { get; }
        
        IToolManager Tools { get; }  
    }
}