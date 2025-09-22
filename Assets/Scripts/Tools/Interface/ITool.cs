using Player.Interface;

namespace Tools.Interface
{
    public interface ITool
    {
        void Initialize(IPlayer player, ISlot slot);
    }
}