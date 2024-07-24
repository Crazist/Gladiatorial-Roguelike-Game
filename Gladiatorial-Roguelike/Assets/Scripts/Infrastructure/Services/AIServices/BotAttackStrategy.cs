using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.BattleServices;
using Logic.Enteties;
using Logic.Entities;
using UI.View;

namespace Infrastructure.Services.AIServices
{
    public class BotAttackStrategy
    {
        private readonly TableService _tableService;

        public BotAttackStrategy(TableService tableService)
        {
            _tableService = tableService;
        }

        public List<AttackInfo>  DetermineAttacks()
        {
            var playerCards = _tableService.GetPlayerTableViews();
            var botCards = _tableService.GetEnemyTableViews();

            var attacks = new List<AttackInfo>();
            var sortedPlayerCards = playerCards.OrderBy(card => card.GetDynamicCardView().GetConcreteTCard().Hp).ToList();

            foreach (var botCard in botCards)
            {
                var botUnitCard = botCard.GetDynamicCardView().GetConcreteTCard();
                if (botUnitCard == null)
                    continue;

                var target = FindKillableTarget(sortedPlayerCards, botUnitCard);

                if (target == null)
                {
                    target = FindMostDangerousTarget(sortedPlayerCards);
                }

                if (target != null)
                {
                    attacks.Add(new AttackInfo { Attacker = botCard, Defender = target });
                    sortedPlayerCards.Remove(target);
                }
            }

            return attacks;
        }

        private CardView FindKillableTarget(List<CardView> sortedPlayerCards, UnitCard botUnitCard)
        {
            return sortedPlayerCards.FirstOrDefault(playerCard =>
                playerCard.GetDynamicCardView().GetConcreteTCard().Hp <= botUnitCard.CardData.UnitData.Attack);
        }

        private CardView FindMostDangerousTarget(List<CardView> sortedPlayerCards)
        {
            return sortedPlayerCards.OrderByDescending(playerCard =>
                playerCard.GetDynamicCardView().GetConcreteTCard().CardData.UnitData.Attack).FirstOrDefault();
        }
    }
}
