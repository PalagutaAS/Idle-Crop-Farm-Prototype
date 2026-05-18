using System;
using Inventor;

namespace Wallets
{
    public class Wallet : IWallet, IValueSource
    {
        private int _count;
        private readonly MoneyType _type;
        
        public Wallet(MoneyType type = MoneyType.Coin, int count = 0)
        {
            _type = type;
            Count = count;
        }

        public int Count
        {
            get => _count;
            private set
            {
                _count = value; 
                OnChangedByTypeForUI?.Invoke((InventoryType)_type, _count);
            }
        }

        public bool Payment(int count)
        {
            if (count > _count) return false;

            Count -= count;
            return true;
        }

        public void Payout(int count)
        {
            Count += count;
        }

        public event Action<InventoryType, int> OnChangedByTypeForUI;
        public int CheckCountByType(InventoryType type)
        {
            return ((InventoryType)_type == type) ? Count : 0;
        }
    }
}