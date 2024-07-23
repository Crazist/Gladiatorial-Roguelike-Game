using Logic.Entities;
using UI.Elements;
using System;
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

        public void ProcessBuff(CardView buffCardView, CardView targetCardView, Action resetPos, bool isPlayer)
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
            CompleteBuff(buffCardView, resetPos, isPlayer);
        }

        public void CompleteBuff(CardView buffCardView, Action resetPos, bool isPlayer)
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

        public void ApplyHealingBuff(UnitCard targetCard, SpecialCard buffCard)
        {
            if (targetCard == null || buffCard == null) return;
            // _buffService.Heal(targetCard, buffCard.SpecialEffectValue);
        }

        public void ApplyStatBuff(UnitCard targetCard, SpecialCard buffCard)
        {
            if (targetCard == null || buffCard == null) return;
            _buffService.Buff(targetCard, buffCard);
        }

        public void ApplyRecruitBuff(UnitCard targetCard, SpecialCard buffCard)
        {
            if (targetCard == null || buffCard == null) return;
            // Implement recruitment logic here
        }
    }
}
