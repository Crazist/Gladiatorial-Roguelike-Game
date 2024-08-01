using Infrastructure.Services.Currency;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.View
{
    public class CurrencyUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currencyText;
        
        private CurrencyService _currencyService;

        [Inject]
        private void Inject(CurrencyService currencyService)
        {
            _currencyService = currencyService;
            _currencyService.OnCurrencyChanged += UpdateCurrencyDisplay;
           
            UpdateCurrencyDisplay();
        }

        private void OnDestroy() => 
            _currencyService.OnCurrencyChanged -= UpdateCurrencyDisplay;

        private void UpdateCurrencyDisplay() => 
            _currencyText.text = $"Currency: {_currencyService.GetCurrency()}";
    }
}