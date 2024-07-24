using System.Collections.Generic;
using Infrastructure.Services.BattleServices;
using UI.View;

namespace Infrastructure.Services.AIServices
{
    public class BotDefenseStrategy
    {
        private readonly TableService _tableService;

        public BotDefenseStrategy(TableService tableService) => _tableService = tableService;

        public List<CardView> DetermineDefense()
        {
            var botCards = _tableService.GetEnemyTableViews();
            var defenseCards = new List<CardView>();

            foreach (var botCard in botCards)
            {
                if (ShouldDefend(botCard) && botCard.GetAttackAndDefence().HasShield)
                {
                    botCard.GetAttackAndDefence().EnableShield();
                    defenseCards.Add(botCard);
                }
            }

            return defenseCards;
        }

        private bool ShouldDefend(CardView botCard)
        {
            var unitCard = botCard.GetDynamicCardView().GetConcreteTCard();
            if (unitCard.Hp < 5 || unitCard.CardData.UnitData.Attack > 8)
            {
                return true;
            }

            return false;
        }
    }
}