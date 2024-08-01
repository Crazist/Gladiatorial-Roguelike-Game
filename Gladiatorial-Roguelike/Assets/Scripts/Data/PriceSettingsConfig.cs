using System.Collections.Generic;
using Logic.Types;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PriceSettingsConfig", menuName = "Settings/Price Settings")]
    public class PriceSettingsConfig : ScriptableObject
    {
        public int HealPricePerHp;
        public int UpgradePriceMultiplier;
        public List<RarityPrice> RarityPrices;

        public int GetPriceForRarity(CardRarity rarity)
        {
            var rarityPrice = RarityPrices.Find(rp => rp.Rarity == rarity);
            return rarityPrice != null ? rarityPrice.Price : 0;
        }
    }
}