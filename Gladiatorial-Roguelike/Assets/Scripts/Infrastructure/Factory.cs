using System;
using System.Collections.Generic;
using System.Linq;
using Data.Cards;
using Infrastructure.Interface;
using Logic.Cards;
using Logic.Enteties;
using Logic.Entities;
using Logic.Types;
using Zenject;

namespace Infrastructure
{
    public class Factory
    {
        private LevelMultiplierConfig _levelMultiplierConfig;
        public List<ISavedProgress> ProgressWriter { get; } = new();
        public List<ISavedProgressReader> ProgressReader { get; } = new();

        [Inject]
        private void Inject(LevelMultiplierConfig levelMultiplierConfig) => 
            _levelMultiplierConfig = levelMultiplierConfig;

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
            card.InitCard(cardData, _levelMultiplierConfig);
            return card;
        }

        private Card CreateSpecialCard(CardData cardData)
        {
            Card card = new SpecialCard();
            card.InitCard(cardData, _levelMultiplierConfig);;
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