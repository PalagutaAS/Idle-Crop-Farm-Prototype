namespace Wallets
{
    public interface IWallet
    {
        bool Payment(int count);
        void Payout(int count);
        int Count { get; }
    }
}