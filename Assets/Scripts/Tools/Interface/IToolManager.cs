using System.Collections.Generic;
using Player.Interface;

namespace Tools.Interface
{
    public interface IToolManager
    {
        bool TrySetupNewTool(ToolType type, int level = 1);
        ISlot GetEmptySlot();
        bool HasEmptySlot();
        public List<ITool> GetAllTools();
    }
}