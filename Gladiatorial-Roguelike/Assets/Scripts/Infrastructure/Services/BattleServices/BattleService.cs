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
        private List<CardView> _defenseCards = new ();
        
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
                if (attacks[i].Defender == null || attacks[i].Attacker == null)
                {
                    continue;
                }

                yield return PerformAttackAnimation(attacks[i].Attacker, attacks[i].Defender);
            }

            _attackService.ClearAttacks();
            DisableAllShields();
        }

        public IEnumerator ExecuteBotAttacks()
        {
            var attacks = _botAttackStrategy.DetermineAttacks(_defenseCards);

            foreach (var attack in attacks)
            {
                _attackService.AddAttack(attack.Attacker, attack.Defender);
            }
            
            yield return new WaitForSeconds(0.5f);
        }

        public IEnumerator ExecuteBotDefense()
        {
            _defenseCards = _botDefenseStrategy.DetermineDefense();

            yield return new WaitForSeconds(0.5f);
        }

        private IEnumerator PerformAttackAnimation(CardView attacker, CardView defender)
        {
            var attackerStartPosition = attacker.transform.position;
            var attackPosition = defender.GetRectTransform().position;

            Tween moveToAttackPosition = attacker.transform.DOMove(attackPosition, 0.5f);
            yield return moveToAttackPosition.WaitForCompletion();

            _damageService.ApplyDamage(attacker, defender);

            Tween moveBackToStartPosition = attacker.transform.DOMove(attackerStartPosition, 0.5f);
            yield return moveBackToStartPosition.WaitForCompletion();
        }

        private void DisableAllShields()
        {
            foreach (var card in _tableService.GetEnemyTableViews())
            {
                if (card != null)
                    card.GetAttackAndDefence().DisableShield();
            }

            foreach (var card in _tableService.GetPlayerTableViews())
            {
                if (card != null)
                    card.GetAttackAndDefence().DisableShield();
            }
        }
    }
}