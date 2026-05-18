using System;

namespace Inventor
{
    public interface IInventory
    {
        public bool HasEnoughByCrop(CropType type, int count);
        public int CheckCountByType(InventoryType type);
    }
    public interface IWallet
    {
        bool Payment(int count);
        void Payout(int count);
        int Count { get; }
    }

    public interface IValueSource
    {
        public event Action<InventoryType, int> OnChangedByTypeForUI;
        public int CheckCountByType(InventoryType type);
    }

    public interface IInventoryChanger : IInventory
    {
        public void Add(CropType type, int count);
        public bool Remove(CropType type, int count);
    }
}