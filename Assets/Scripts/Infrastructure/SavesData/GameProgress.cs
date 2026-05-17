using System;

namespace SavesData
{
    [Serializable]
    public class GameProgress
    {
        public InventoryData InventoryData;
    }

    [Serializable]
    public class InventoryData
    {
        public int Gold;
        public int Wheat;
        public int Potato;
        public int Corn;
    }
}