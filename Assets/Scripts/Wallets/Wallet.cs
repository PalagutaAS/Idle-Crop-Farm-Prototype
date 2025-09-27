using DefaultNamespace;

namespace Wallets
{
    public class Wallet : IWallet
    {
        private int _count;

        public Wallet(int count)
        {
            Count = count;
        }

        public int Count
        {
            get => _count;
            private set
            {
                _count = value; 
                EventsHolder.UpdateWallet.Invoke(_count);
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
    }
}