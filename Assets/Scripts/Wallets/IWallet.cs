using System;

namespace Wallets
{
    public interface IWallet
    {
        public event Action<int> OnChanged;
        bool Payment(int count);
        void Payout(int count);
        int Count { get; }
    }

    public interface IChangedInventory
    {
        public event Action<CropType, int> OnChangedByType;
    }
}