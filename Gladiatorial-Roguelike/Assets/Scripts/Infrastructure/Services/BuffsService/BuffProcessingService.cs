using Logic.Entities;
using System;
using Infrastructure.Services.BattleServices;
using Logic.Enteties;
using Logic.Types;
using UI.View;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.BuffsService
{
    public class BuffProcessingService
    {
        private BuffService _buffService;
        private TableService _tableService;

        public BuffProcessingService(BuffService buffService, TableService tableService)
        {
            _buffService = buffService;
            _tableService = tableService;
        }

        public void ProcessBuff(CardView buffCardView, CardView targetCardView, bool isPlayer, Action resetPos = null)
        {
            var targetCard = targetCardView.GetCard() as UnitCard;
            var buffCard = buffCardView.GetCard() as SpecialCard;

            switch (buffCardView.GetCard().CardData.CardType)
            {
                case CardType.Healing:
                    ApplyHealingBuff(targetCard, buffCard);
                    break;
                case CardType.Buff:
                    ApplyStatBuff(targetCard, buffCard);
                    break;
                case CardType.Recruit:
                    ApplyRecruitBuff(targetCard, buffCard);
                    break;
            }

            targetCardView.UpdateView();
            CompleteBuff(buffCardView, isPlayer, resetPos);
        }

        private void CompleteBuff(CardView buffCardView, bool isPlayer, Action resetPos = null)
        {
            if (isPlayer)
            {
                _tableService.GetPlayerHandViews().Remove(buffCardView);
            }
            else
            {
                _tableService.GetEnemyHandViews().Remove(buffCardView);
            }
            
            Object.Destroy(buffCardView.gameObject);
            resetPos?.Invoke();
        }

        private void ApplyHealingBuff(UnitCard targetCard, SpecialCard buffCard)
        {
            if (targetCard == null || buffCard == null) return;
            // _buffService.Heal(targetCard, buffCard.SpecialEffectValue);
        }

        private void ApplyStatBuff(UnitCard targetCard, SpecialCard buffCard)
        {
            if (targetCard == null || buffCard == null) return;
            _buffService.Buff(targetCard, buffCard);
            buffCard.GainXP(buffCard.CardData.SpecialData.XpWithUse);
        }

        private void ApplyRecruitBuff(UnitCard targetCard, SpecialCard buffCard)
        {
            if (targetCard == null || buffCard == null) return;
            // Implement recruitment logic here
        }
    }
}
