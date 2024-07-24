using System.Collections.Generic;
using System.Linq;
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
            var playerCards = _tableService.GetPlayerTableViews();
            var defenseCards = new List<CardView>();

            var highPriorityPlayerCards = DetermineHighPriorityPlayerCards(playerCards);

            foreach (var botCard in botCards)
            {
                if (ShouldDefend(botCard, highPriorityPlayerCards))
                {
                    botCard.GetAttackAndDefence().EnableShield();
                    defenseCards.Add(botCard);
                }
            }

            return defenseCards;
        }

        private List<CardView> DetermineHighPriorityPlayerCards(List<CardView> playerCards)
        {
            return playerCards
                .Where(card => !card.GetAttackAndDefence().HasShield)
                .OrderByDescending(card => card.GetDynamicCardView().GetConcreteTCard().CardData.UnitData.Attack)
                .ToList();
        }

        private bool ShouldDefend(CardView botCard, List<CardView> highPriorityPlayerCards)
        {
            if (botCard.GetAttackAndDefence().HasShield || botCard.GetAttackAndDefence().ShieldBroken)
            {
                return false;
            }

            var attackTargets = DeterminePotentialAttackTargets(botCard);
            if (attackTargets.Any())
            {
                return false;
            }

            double attackProbability = CalculateAttackProbability(botCard, highPriorityPlayerCards);
            double defendPriority = CalculateDefendPriority(botCard, attackProbability);

            return defendPriority > 0.5;
        }

        private List<CardView> DeterminePotentialAttackTargets(CardView botCard)
        {
            var playerCards = _tableService.GetPlayerTableViews();
            var botUnitCard = botCard.GetDynamicCardView().GetConcreteTCard();
            var attackTargets = new List<CardView>();

            foreach (var playerCard in playerCards)
            {
                var playerUnitCard = playerCard.GetDynamicCardView().GetConcreteTCard();
                if (playerUnitCard.Hp <= botUnitCard.CardData.UnitData.Attack)
                {
                    attackTargets.Add(playerCard);
                }
            }

            return attackTargets;
        }

        private double CalculateAttackProbability(CardView botCard, List<CardView> highPriorityPlayerCards)
        {
            var botUnitCard = botCard.GetDynamicCardView().GetConcreteTCard();
            double probability = 0.0;

            foreach (var playerCard in highPriorityPlayerCards)
            {
                var playerUnitCard = playerCard.GetDynamicCardView().GetConcreteTCard();
                probability += playerUnitCard.CardData.UnitData.Attack / (double)botUnitCard.Hp;
            }

            return probability / highPriorityPlayerCards.Count;
        }

        private double CalculateDefendPriority(CardView botCard, double attackProbability)
        {
            var botUnitCard = botCard.GetDynamicCardView().GetConcreteTCard();

            double defendPriority = botUnitCard.CardData.UnitData.Attack * (1.0 - attackProbability);

            defendPriority *= (botUnitCard.Hp / (double)botUnitCard.CardData.UnitData.Hp);

            return defendPriority;
        }
    }
}
