using System.Collections;
using DG.Tweening;
using Infrastructure.Services.AIServices;
using Logic.Entities;
using UI.Factory;
using UI.View;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.BattleServices
{
    public class BattleService
    {
        private AttackService _attackService;
        private UIFactory _uiFactory;
        private DamageService _damageService;
        private BotAttackStrategy _botAttackStrategy;
        private TableService _tableService;

        [Inject]
        private void Inject(AttackService attackService, UIFactory uiFactory, DamageService damageService,
            TableService tableService)
        {
            _attackService = attackService;
            _uiFactory = uiFactory;
            _damageService = damageService;
            _tableService = tableService;

            _botAttackStrategy = new BotAttackStrategy(tableService);
        }

        public IEnumerator CalculateAttacks()
        {
            foreach (var attack in _attackService.GetAttacks())
            {
                if (attack.Defender == null || attack.Defender.GetDynamicCardView() == null)
                {
                    continue;
                }

                yield return PerformAttackAnimation(attack.Attacker, attack.Defender);
            }

            _attackService.ClearAttacks();
        }

        private IEnumerator PerformAttackAnimation(CardView attacker, CardView defender)
        {
            var attackerStartPosition = attacker.transform.position;
            var attackPosition = defender.GetRectTransform().position;

            Tween moveToAttackPosition = attacker.transform.DOMove(attackPosition, 0.5f);
            yield return moveToAttackPosition.WaitForCompletion();

            if (defender != null && defender.GetDynamicCardView() != null)
            {
                _damageService.ApplyDamage(attacker, defender);
            }

            Tween moveBackToStartPosition = attacker.transform.DOMove(attackerStartPosition, 0.5f);
            yield return moveBackToStartPosition.WaitForCompletion();
        }

        public IEnumerator ExecuteBotAttacks()
        {
            var attacks = _botAttackStrategy.DetermineAttacks();

            foreach (var attack in attacks)
            {
                _attackService.AddAttack(attack.Attacker, attack.Defender);
                yield return PerformAttackAnimation(attack.Attacker, attack.Defender);
            }

            _attackService.ClearAttacks();
        }
    }
}