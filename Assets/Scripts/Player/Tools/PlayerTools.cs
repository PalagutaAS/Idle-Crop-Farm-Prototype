using System.Collections.Generic;
using System.Linq;
using Player.Interface;
using Player.Slots;
using Tools.Interface;
using Tools.ScriptableObjects;
using UnityEngine;
using VContainer;

namespace Player.Tools
{
    public class PlayerTools : MonoBehaviour, IToolManager
    {
        [SerializeField] private Slot[] _slots;
    
        private LibraryToolConfigs _libraryToolConfigs;
        private IToolFactory _toolFactory;
        private IPlayer _player;
        
        public int CountSlots => _slots.Length;

        [Inject]
        public void Constructor(IPlayer player, IToolFactory toolFactory, LibraryToolConfigs libraryToolConfigs)
        {
            _player = player;
            _toolFactory = toolFactory;
            _libraryToolConfigs = libraryToolConfigs;
        }
    
        public bool TrySetupNewTool(ToolType type, int level = 1)
        {
            if (level == 0)
                return false;
            
            if (HasToolOfType(type) || !HasEmptySlot()) 
                return false;
            
            ISlot freeSlot = GetEmptySlot();

            var config = _libraryToolConfigs.GetConfigByLevel(type,level);
            ITool newTool = _toolFactory.CreateTool(type);
            newTool.Initialize(_player, freeSlot, config);
        
            freeSlot.SetTool(newTool);
            return true;
        }

        public ISlot GetEmptySlot()
        {
            return _slots.FirstOrDefault(s => !s.IsOccupied);
        }

        public bool HasEmptySlot()
        {
            return (_slots.FirstOrDefault(s => !s.IsOccupied) != null);
        }

        public bool HasToolOfType(ToolType type)
        {
            return _slots.Any(s => s.IsOccupied && s.CurrentTool.Type == type);
        }

        public List<ITool> GetAllTools()
        {
            List<ITool> tools = new List<ITool>();
            foreach (var slot in _slots)
            {
                if (!slot.IsOccupied) continue;
                tools.Add(slot.CurrentTool);
            }

            return tools;
        }

    }
}