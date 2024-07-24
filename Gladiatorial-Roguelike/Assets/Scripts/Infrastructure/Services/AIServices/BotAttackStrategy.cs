using System.Collections.Generic;
using System.Linq;
using Infrastructure.Services.BattleServices;
using Logic.Enteties;
using UI.View;

namespace Infrastructure.Services.AIServices
{
    public class BotAttackStrategy
    {
        private readonly TableService _tableService;

        public BotAttackStrategy(TableService tableService) => _tableService = tableService;

        public List<AttackInfo> DetermineAttacks(List<CardView> defenseCards)
        {
            var playerCards = _tableService.GetPlayerTableViews();
            var botCards = _tableService.GetEnemyTableViews().Except(defenseCards).ToList();

            var attacks = new List<AttackInfo>();
            var remainingPlayerCards = new List<CardView>(playerCards);
            var remainingBotCards = new List<CardView>(botCards);

            AttackWithMinimumHits(remainingPlayerCards, remainingBotCards, attacks);
            AttackRandomTargets(remainingPlayerCards, remainingBotCards, attacks);

            return attacks;
        }

        private void AttackWithMinimumHits(List<CardView> remainingPlayerCards, List<CardView> remainingBotCards, List<AttackInfo> attacks)
        {
            var attackQueue = new List<CardView>(remainingBotCards);

            while (remainingPlayerCards.Count > 0 && attackQueue.Count > 0)
            {
                var target = FindBestTarget(remainingPlayerCards, attackQueue);
                if (target == null) break;

                int requiredHits = CalculateRequiredHits(target, attackQueue);
                var attackers = attackQueue.Take(requiredHits).ToList();

                foreach (var attacker in attackers)
                {
                    attacks.Add(new AttackInfo { Attacker = attacker, Defender = target });
                    attackQueue.Remove(attacker);
                }

                remainingPlayerCards.Remove(target);
            }
        }

        private void AttackRandomTargets(List<CardView> remainingPlayerCards, List<CardView> remainingBotCards, List<AttackInfo> attacks)
        {
            foreach (var botCard in remainingBotCards)
            {
                var randomTarget = remainingPlayerCards.OrderBy(_ => System.Guid.NewGuid()).FirstOrDefault();
                if (randomTarget != null)
                {
                    attacks.Add(new AttackInfo { Attacker = botCard, Defender = randomTarget });
                }
            }
        }

        private CardView FindBestTarget(List<CardView> remainingPlayerCards, List<CardView> attackQueue)
        {
            foreach (var playerCard in remainingPlayerCards)
            {
                int requiredHits = CalculateRequiredHits(playerCard, attackQueue);
                if (requiredHits <= attackQueue.Count)
                {
                    return playerCard;
                }
            }

            return null;
        }

        private int CalculateRequiredHits(CardView playerCard, List<CardView> attackQueue)
        {
            int requiredHits = 0;
            int totalDamage = 0;
            int remainingHp = playerCard.GetDynamicCardView().GetConcreteTCard().Hp;
            int shieldHp = playerCard.GetAttackAndDefence().HasShield ? playerCard.GetDynamicCardView().GetConcreteTCard().CardData.UnitData.Defense : 0;

            var sortedAttackQueue = attackQueue.OrderBy(c => c.GetDynamicCardView().GetConcreteTCard().CardData.UnitData.Attack).ToList();

            foreach (var attacker in sortedAttackQueue)
            {
                totalDamage += attacker.GetDynamicCardView().GetConcreteTCard().CardData.UnitData.Attack;
                requiredHits++;

                if (shieldHp > 0)
                {
                    if (totalDamage >= shieldHp)
                    {
                        totalDamage -= shieldHp;
                        shieldHp = 0;
                    }
                }
                else
                {
                    remainingHp -= totalDamage;
                    if (remainingHp <= 0)
                    {
                        break;
                    }
                }
            }

            return requiredHits;
        }
    }
}
