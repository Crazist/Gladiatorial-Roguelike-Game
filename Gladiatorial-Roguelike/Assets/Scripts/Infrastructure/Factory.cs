using System.Collections.Generic;
using System.Linq;
using Infrastructure.Interface;
using Infrastructure.Services;
using Logic.Cards;
using Logic.Enteties;
using Zenject;

namespace Infrastructure
{
    public class Factory
    {
        public List<ISavedProgress> ProgressWriter { get; } = new();
        public List<ISavedProgressReader> ProgressReader { get; } = new();
        
        private StaticDataService _staticDataService;

        [Inject]
        private void Inject(StaticDataService staticDataService) => 
            _staticDataService = staticDataService;

        public Card CreateCard(CardData cardData) => 
            new Card(cardData);

        public List<Card> CreateCards(IEnumerable<CardData> cardDataList) => 
            cardDataList.Select(cardData => new Card(cardData)).ToList();

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriter.Add(progressWriter);
            
            ProgressReader.Add(progressReader);
        }
    }
}