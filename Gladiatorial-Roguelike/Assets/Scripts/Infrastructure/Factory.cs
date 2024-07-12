using System.Collections.Generic;
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
            new(cardData);

        private void Register(ISavedProgressReader progressReader)
        {
            if(progressReader is ISavedProgress progressWriter)
                ProgressWriter.Add(progressWriter);
            
            ProgressReader.Add(progressReader);
        }
    }
}