using System.Collections.Generic;
using Player.Interface;

namespace Tools.Interface
{
    public interface IToolManager
    {
        bool TrySetupNewTool(ToolType type, int level = 1);
        ISlot GetEmptySlot();
        bool HasEmptySlot();
        bool HasToolOfType(ToolType type); 
        public List<ITool> GetAllTools();
    }
}