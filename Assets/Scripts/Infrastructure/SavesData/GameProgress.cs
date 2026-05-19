using System;

namespace SavesData
{
    [Serializable]
    public class GameProgress
    {
        public InventoryData InventoryData;
        public WalletData WalletData;

        public GameProgress()
        {
        }
    }

    [Serializable]
    public class WalletData
    {
        public int Gold;
        public int Emirald;
    }

    [Serializable]
    public class InventoryData
    {
        public int Wheat;
        public int Potato;
        public int Corn;
    }
}