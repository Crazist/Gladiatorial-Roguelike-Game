using System.Collections.Generic;
using System.Linq;
using Logic.Entities;
using Logic.Types;

namespace Services
{
    public class CardSortingService
    {
        public Dictionary<Card, int> GroupAndSortCards(List<Card> cards)
        {
            var groupedCards = cards
                .GroupBy(card => new { card.CardRarity, card.CardName })
                .Select(group => new
                {
                    Card = group.First(),
                    Count = group.Count()
                })
                .OrderBy(card => GetRarityOrder(card.Card.CardRarity))
                .ThenBy(card => card.Card.CardName)
                .ToDictionary(card => card.Card, card => card.Count);

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