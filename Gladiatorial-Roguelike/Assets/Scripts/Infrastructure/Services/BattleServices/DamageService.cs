using Logic.Types;
using UI.View;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.BattleServices
{
    public class DamageService
    {
        private TableService _tableService;

        [Inject]
        private void Inject(TableService tableService) => _tableService = tableService;

        public void ApplyDamage(CardView attackerView, CardView defenderView)
        {
            var attackerUnit = attackerView.GetDynamicCardView().GetConcreteTCard();
            var defenderUnit = defenderView.GetDynamicCardView().GetConcreteTCard();
            var attackAndDefence = defenderView.GetAttackAndDefence();

            if (attackerUnit != null && defenderUnit != null)
            {
                if (attackAndDefence.HasShield)
                {
                    int remainingDamage = attackerUnit.CardData.UnitData.Attack - defenderUnit.CardData.UnitData.Defense;
                    if (remainingDamage > 0)
                    {
                        defenderUnit.TakeDamage(remainingDamage);
                        attackAndDefence.BreakShield();
                    }
                    else
                    {
                        attackAndDefence.DisableShield();
                    }
                }
                else
                {
                    defenderUnit.TakeDamage(attackerUnit.CardData.UnitData.Attack);
                }

                defenderView.GetDynamicCardView().UpdateHp();

                if (defenderUnit.Hp <= 0)
                {
                    RemoveFromTable(defenderView);
                    DestroyCard(defenderView);
                }
            }
        }

        private void RemoveFromTable(CardView defenderView)
        {
            if (defenderView.Team == TeamType.Ally)
            {
                _tableService.GetPlayerTableViews().Remove(defenderView);
            }
            else
            {
                _tableService.GetEnemyTableViews().Remove(defenderView);
            }
        }

        private void DestroyCard(CardView cardView) => 
            Object.Destroy(cardView.gameObject);
    }
}
