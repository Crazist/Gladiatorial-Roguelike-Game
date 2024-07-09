using Infrastructure.Services;
using Logic.Cards;
using Logic.Enteties;
using Zenject;

namespace Infrastructure
{
    public class Factory
    {
        private StaticDataService _staticDataService;

        [Inject]
        private void Inject(StaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public Card CreateCard(CardData cardData) => 
            new(cardData);
    }
}