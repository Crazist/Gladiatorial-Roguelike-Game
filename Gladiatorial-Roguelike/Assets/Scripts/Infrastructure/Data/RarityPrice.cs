using System;
using Logic.Types;

namespace Infrastructure.Data
{
    [Serializable]
    public class RarityPrice
    {
        public CardRarity Rarity;
        public int Price;
    }
}