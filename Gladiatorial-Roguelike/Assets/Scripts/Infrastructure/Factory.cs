using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Interface;
using Logic.Cards;
using Logic.Entities;

namespace Infrastructure
{
    public class Factory
    {
        public List<ISavedProgress> ProgressWriter { get; } = new();
        public List<ISavedProgressReader> ProgressReader { get; } = new();

        public List<Card> CreateCards(IEnumerable<CardData> cardDataList) =>
            cardDataList.Select(CreateCard).ToList();

        public Card CreateCard(CardData cardData)
        {
            switch (cardData)
            {
                case UnitCardData unitCardData:
                    return CreateUnitCard(unitCardData);
                case SpecialCardData specialCardData:
                    return CreateSpecialCard(specialCardData);
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardData));
            }
        }

        private Card CreateUnitCard(UnitCardData cardData)
        {
            Card card = new UnitCard();
            card.InitCard(cardData);
            return card;
        }

        private Card CreateSpecialCard(SpecialCardData cardData)
        {
            Card card = new SpecialCard();
            card.InitCard(cardData);;
            return card;
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriter.Add(progressWriter);

            ProgressReader.Add(progressReader);
        }
    }
}