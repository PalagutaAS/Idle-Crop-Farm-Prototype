using System;
using UnityEngine;
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
            YG2.GameplayStop();
        }

        private void OnShowWindowGame()
        {
            if (Time.timeScale == 0)
                return;
            
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