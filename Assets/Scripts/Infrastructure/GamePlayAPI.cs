using System;
using Logging;
using YG;

namespace Infrastructure
{
    public class GamePlayAPI : IGamePlayAPI, IDisposable
    {
        public GamePlayAPI()
        {
            YG2.onShowWindowGame += OnShowWindowGame;
            YG2.onHideWindowGame += OnHideWindowGame;
        }

        private void OnHideWindowGame()
        {
            this.Log("Hide Window Game!");
            YG2.GameplayStop();
        }

        private void OnShowWindowGame()
        {
            this.Log("Show Window Game!");
            YG2.GameplayStart();
        }

        public void Dispose()
        {
            YG2.onShowWindowGame -= OnShowWindowGame;
            YG2.onHideWindowGame -= OnHideWindowGame;
        }
    }

    public interface IGamePlayAPI
    {
    }
}