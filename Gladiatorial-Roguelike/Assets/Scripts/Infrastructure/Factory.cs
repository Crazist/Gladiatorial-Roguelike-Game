using System;
using System.Collections.Generic;
using System.Linq;
using Data.Cards;
using Infrastructure.Interface;
using Logic.Cards;
using Logic.Entities;
using Logic.Types;

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
            switch (cardData.Category)
            {
                case CardCategory.Unit:
                    return CreateUnitCard(cardData);
                case CardCategory.Special:
                    return CreateSpecialCard(cardData);
                default:
                    throw new ArgumentOutOfRangeException(nameof(cardData));
            }
        }

        private Card CreateUnitCard(CardData cardData)
        {
            Card card = new UnitCard();
            card.InitCard(cardData);
            return card;
        }

        private Card CreateSpecialCard(CardData cardData)
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