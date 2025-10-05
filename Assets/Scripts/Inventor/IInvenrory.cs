using System;

namespace Inventor
{
    public interface IWallet
    {
        bool Payment(int count);
        void Payout(int count);
        int Count { get; }
    }

    public interface IValueSource
    {
        public event Action<InventoryType, int> OnChangedByType;
        public int CheckCountByType(InventoryType type);
    }

    public interface IInventoryChanger
    {
        public void Add(CropType type, int count);
        public bool Remove(CropType type, int count);
        public bool HasEnoughByCrop(CropType type, int count);
    }
}