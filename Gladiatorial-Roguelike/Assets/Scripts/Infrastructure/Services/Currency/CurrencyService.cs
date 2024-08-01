using System;
using Infrastructure.Data;
using Infrastructure.Services.PersistentProgress;
using Zenject;

namespace Infrastructure.Services.Currency
{
    public class CurrencyService
    {
        public event Action OnCurrencyChanged;
       
        private SaveLoadService _saveLoadService;
        private PersistentProgressService _persistent;

        [Inject]
        private void Inject(PersistentProgressService persistent, SaveLoadService saveLoadService)
        {
            _persistent = persistent;
            _saveLoadService = saveLoadService;
         }

        public void AddCurrency(int amount)
        {
            if (amount < 0) return;

            _persistent.PlayerProgress.Profile.Currency += amount;
            OnCurrencyChanged?.Invoke();
        }

        public void SpendCurrency(int amount)
        {
            if (amount < 0) return;

            if (_persistent.PlayerProgress.Profile.Currency >= amount)
                _persistent.PlayerProgress.Profile.Currency -= amount;
            else
                _persistent.PlayerProgress.Profile.Currency = 0;

            OnCurrencyChanged?.Invoke();
        }

        public int GetCurrency() =>
            _persistent.PlayerProgress.Profile.Currency;

        private void SaveCurrency() => 
            _saveLoadService.SaveProgress();
    }
}