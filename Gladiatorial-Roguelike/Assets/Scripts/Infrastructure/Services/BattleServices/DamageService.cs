using Logic.Types;
using UI.View;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.BattleServices
{
    public class DamageService
    {
        private TableService _tableService;
        private BattleResultService _battleResultService;

        [Inject]
        private void Inject(TableService tableService, BattleResultService battleResultService)
        {
            _battleResultService = battleResultService;
            _tableService = tableService;
        }

        public void ApplyDamage(CardView attackerView, CardView defenderView)
        {
            var attackerUnit = attackerView.GetDynamicCardView().GetConcreteTCard();
            var defenderUnit = defenderView.GetDynamicCardView().GetConcreteTCard();
           
            var attackAndDefence = defenderView.GetAttackAndDefence();

            if (attackerUnit != null && defenderUnit != null)
            {
                int attackDamage = attackerUnit.Attack;

                if (attackAndDefence.HasShield)
                { 
                    defenderUnit.Defense -= attackDamage;
                    
                    if (defenderUnit.Defense > 0)
                    {
                        attackAndDefence.UpdateShieldStrength(defenderUnit.Defense);
                    }
                    else
                    {
                        int remainingDamage = attackDamage - defenderUnit.CardData.UnitData.Defense;
                        attackAndDefence.BreakShield();
                        
                        if (remainingDamage > 0)
                        {
                            defenderUnit.TakeDamage(remainingDamage);
                        }
                    }
                }
                else
                {
                    defenderUnit.TakeDamage(attackDamage);
                }

                defenderView.GetDynamicCardView().UpdateCard();

                if (defenderUnit.Hp <= 0)
                {
                    SetPlayerLose(defenderView);
                    RemoveFromTable(defenderView);
                    DestroyCard(defenderView);

                    attackerUnit.GainXP(defenderUnit.CardData.UnitData.XPWithKill);
                    attackerView.GetDynamicCardView().UpdateCard();
                    attackerView.GetCardDisplay().UpgradeImage();
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

        private void SetPlayerLose(CardView cardView)
        {
            if (cardView.Team == TeamType.Ally)
            {
                _battleResultService.PlayerLost.Add(cardView.GetCard());
            }
        }
        
        private void DestroyCard(CardView cardView) => 
            Object.Destroy(cardView.gameObject);
    }
}
