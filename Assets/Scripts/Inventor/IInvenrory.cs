using System;

namespace Inventor
{
    public interface IInventory : IValueSource<CropType>
    {
        public bool HasEnoughByCrop(CropType type, int count);
    }
    public interface IWallet : IValueSource<MoneyType>
    {
        bool Payment(int count);
        void Payout(int count);
        int Count { get; }
    }

    public interface IValueSource<TKey>
    {
        event Action<TKey, int> OnChangedByTypeForUI;
        int CheckCountByType(TKey type);
    }

    public interface IInventoryChanger : IInventory
    {
        public void Add(CropType type, int count);
        public bool Remove(CropType type, int count);
    }
}