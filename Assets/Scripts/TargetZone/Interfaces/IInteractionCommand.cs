using AI;
using Player.Interface;

namespace TargetZone.Interfaces
{
    public interface IInteractionCommand
    {
        string Title { get; }
        bool CanExecute(IPlayer player, CustomerController customer = null);
        void Execute(IPlayer player, CustomerController customer = null);
    }
}