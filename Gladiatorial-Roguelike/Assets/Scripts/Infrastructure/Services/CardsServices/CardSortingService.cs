using System.Collections.Generic;
using System.Linq;
using Logic.Entities;
using Logic.Types;

namespace Services
{
    public class CardSortingService
    {
        public List<Card> GroupAndSortCards(List<Card> cards)
        {
            var groupedCards = cards
                .GroupBy(card => new { card.CardType, card.CardRarity, card.CardName })
                .SelectMany(group => group)
                .OrderBy(card => GetRarityOrder(card.CardRarity))
                .ThenBy(card => card.CardType)
                .ThenBy(card => card.CardName)
                .ToList();

            return groupedCards;
        }

        private int GetRarityOrder(CardRarity rarity)
        {
            return rarity switch
            {
                CardRarity.Normal => 1,
                CardRarity.Rare => 2,
                CardRarity.Epic => 3,
                CardRarity.Legendary => 4,
                _ => 5,
            };
        }
    }
}