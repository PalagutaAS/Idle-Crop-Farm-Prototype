using Player.Interface;

namespace Tools.Interface
{
    public interface ITool
    {
        void Initialize(IPlayer player, ISlot slot, IToolConfig config);
        void Upgrade(IToolConfig config);
        int CurrentLevel { get; }
        ToolType Type { get; }
    }
}