using System.Collections;
using DG.Tweening;
using UI.Factory;
using UI.View;
using Zenject;

namespace Infrastructure.Services.BattleServices
{
    public class BattleService
    {
        private AttackService _attackService;
        private UIFactory _uiFactory;
        private DamageService _damageService;

        [Inject]
        private void Inject(AttackService attackService, UIFactory uiFactory, DamageService damageService)
        {
            _attackService = attackService;
            _uiFactory = uiFactory;
            _damageService = damageService;
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
    }
}