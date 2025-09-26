using Player.Interface;

namespace TargetZone.Interfaces
{
    public interface IInteractionCommand
    {
        string Title { get; }
        bool CanExecute(IPlayer player);
        void Execute(IPlayer player);
    }
}