using System;
using Logic.Types;

namespace Data
{
    [Serializable]
    public class RarityPrice
    {
        public CardRarity Rarity;
        public int Price;
    }
}