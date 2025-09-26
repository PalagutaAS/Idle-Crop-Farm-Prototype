using System.Collections.Generic;
using Player.Interface;

namespace Tools.Interface
{
    public interface IToolManager
    {
        bool TrySetupNewTool(ToolType type);
        ISlot GetEmptySlot();
        bool HasEmptySlot();
        public List<ITool> GetAllTools();
    }
}