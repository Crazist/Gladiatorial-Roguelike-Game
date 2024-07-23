using UI.View;
using UnityEngine;

namespace Infrastructure.Services.BattleServices
{
    public class DamageService
    {
        public void ApplyDamage(CardView attackerView, CardView defenderView)
        {
            var attackerUnit = attackerView.GetDynamicCardView().GetConcreteTCard();
            var defenderUnit = defenderView.GetDynamicCardView().GetConcreteTCard();

            if (attackerUnit != null && defenderUnit != null)
            {
                defenderUnit.TakeDamage(attackerUnit.CardData.UnitData.Attack);
                defenderView.GetDynamicCardView().UpdateHp();

                if (defenderUnit.Hp <= 0)
                {
                    DestroyCard(defenderView);
                }
            }
        }

        private void DestroyCard(CardView cardView) => 
            Object.Destroy(cardView.gameObject);
    }
}