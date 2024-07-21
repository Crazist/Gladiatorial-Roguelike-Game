using Logic.Entities;
using UI.Elements;
using System;
using Logic.Types;
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

        public void ProcessBuff(CardView buffCardView, CardView targetCardView, Action resetPos)
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
            CompleteBuff(buffCardView, resetPos);
        }

        public void CompleteBuff(CardView buffCardView, Action resetPos)
        {
            _tableService.RemoveCardFromPlayerHand(buffCardView.GetCard());
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
