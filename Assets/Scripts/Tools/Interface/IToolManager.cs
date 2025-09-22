using Player.Interface;

namespace Tools.Interface
{
    public interface IToolManager
    {
        bool TrySetupNewTool();
        ISlot GetEmptySlot();
    }
}