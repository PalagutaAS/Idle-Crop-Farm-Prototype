using Player.Interface;
using TargetZone.Interfaces;
using UnityEngine;

namespace TargetZone.Command
{
    public class BuyNewFieldCommand : IInteractionCommand
    {
        public string Title { get; }
        
        public BuyNewFieldCommand(string title)
        {
            Title = $"Buy: {title}";
        }
        public bool CanExecute(IPlayer player)
        {
            return player.Wallet.Count >= 1000;
        }

        public void Execute(IPlayer player)
        {
            if (CanExecute(player))
            { 
                Debug.Log("You bought a field");
            }
        }

    }
}