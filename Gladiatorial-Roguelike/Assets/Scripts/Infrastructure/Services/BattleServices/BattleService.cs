using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.Services.AIServices;
using Logic.Enteties;
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
        private BotDefenseStrategy _botDefenseStrategy;
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
            _botDefenseStrategy = new BotDefenseStrategy(tableService);
        }

        public IEnumerator CalculateAttacks()
        {
            List<AttackInfo> attacks = _attackService.GetAttacks();

            for (int i = 0; i < attacks.Count; i++)
            {
                if (attacks[i].Defender == null || attacks[i].Defender.GetDynamicCardView() == null)
                {
                    continue;
                }

                yield return PerformAttackAnimation(attacks[i].Attacker, attacks[i].Defender);
                
                if (attacks[i].Defender == null)
                {
                    RemoveRelatedAttacks(attacks, attacks[i].Defender);
                }
            }

            _attackService.ClearAttacks();
            DisableAllShields();
        }

        public IEnumerator ExecuteBotActions()
        {
            var defenseCards = _botDefenseStrategy.DetermineDefense();
            
            yield return new WaitForSeconds(0.5f);

            var attacks = _botAttackStrategy.DetermineAttacks(defenseCards);
            
            foreach (var attack in attacks)
            {
                _attackService.AddAttack(attack.Attacker, attack.Defender);
            }
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

        private void DisableAllShields()
        {
            foreach (var card in _tableService.GetEnemyTableViews())
            {
                card.GetAttackAndDefence().DisableShield();
            }
            foreach (var card in _tableService.GetPlayerTableViews())
            {
                card.GetAttackAndDefence().DisableShield();
            }
        }
        private void RemoveRelatedAttacks(List<AttackInfo> attacks, CardView card)
        {
            for (int i = attacks.Count - 1; i >= 0; i--)
            {
                if (attacks[i].Attacker == card || attacks[i].Defender == card)
                {
                    attacks.RemoveAt(i);
                }
            }
        }
    }
}
