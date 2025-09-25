using Player.Interface;
using Tools.Interface;
using UnityEngine;

namespace Player.Slots
{
    public class Slot : MonoBehaviour, ISlot
    {
        private ITool _currentTool;
        
        public Transform Transform => transform;
        public bool IsOccupied => _currentTool != null;
        public ITool CurrentTool => _currentTool;
        
        public void SetTool(ITool currentTool)
        {
            _currentTool = currentTool;
        }

        public void RemoveTool()
        {
            _currentTool = null;
        }
    }
}