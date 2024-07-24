using Logic.Entities;
using Logic.Types;
using UI.Elements;
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
            UnitCard attackerUnit = attackerView.GetDynamicCardView().GetConcreteTCard();
            UnitCard defenderUnit = defenderView.GetDynamicCardView().GetConcreteTCard();

            if (attackerUnit != null && defenderUnit != null)
            {
                int attackDamage = attackerUnit.CardData.UnitData.Attack;
                int shieldDefense = defenderUnit.CardData.UnitData.Defense;
               
                AttackAndDefence attackAndDefence = defenderView.GetAttackAndDefence();

                if (attackAndDefence.HasShield)
                {
                    if (shieldDefense >= attackDamage)
                    {
                        attackAndDefence.CleanUp();
                        return;
                    }
                    else
                    {
                        attackAndDefence.CleanUp();
                        attackDamage -= shieldDefense;
                    }
                }

                defenderUnit.TakeDamage(attackDamage);
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
