using Inventor;
using Player.Tools;
using Tools.Interface;
using UnityEngine;
using Wallets;

namespace Player.Interface
{
    public interface IPlayer
    {
        Transform Transform { get; }
        Inventory Inventory { get; }
        Wallet Wallet { get; }
        
        IToolManager Tools { get; }  
    }
}