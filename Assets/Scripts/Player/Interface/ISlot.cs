using Tools.Interface;
using UnityEngine;

namespace Player.Interface
{
    public interface ISlot
    {
        Transform Transform { get; }
        bool IsOccupied { get; }
        
        ITool CurrentTool { get; }

        void SetTool(ITool currentTool);
        void RemoveTool();
    }
}