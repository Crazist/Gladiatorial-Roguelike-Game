using System;
using System.Collections;
using Infrastructure.Services.BuffsService;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using Zenject;
using Object = UnityEngine.Object;

namespace Infrastructure.Services.CardsServices
{
    public class CardBuffService
    {
        private TableService _tableService;
        private CoroutineCustomRunner _coroutineCustomRunner;
        private BuffService _buffService;

        [Inject]
        public void Inject(TableService tableService, CoroutineCustomRunner coroutineCustomRunner, BuffService buffService)
        {
            _buffService = buffService;
            _coroutineCustomRunner = coroutineCustomRunner;
            _tableService = tableService;
        }

        public void ApplyBuff(CardView buffCardView, Action resetPos)
        {
            buffCardView.ChangeRaycasts(false);
            _coroutineCustomRunner.StartCoroutine(ApplyBuffCoroutine(buffCardView, resetPos));
        }

        private IEnumerator ApplyBuffCoroutine(CardView buffCardView, Action resetPos)
        {
            yield return null;

            var hoveredCardView = _tableService.GetHoveredCard();

            if (hoveredCardView == null || hoveredCardView.GetCard().CardData.Category != CardCategory.Unit)
            {
                buffCardView.ChangeRaycasts(true);
                resetPos.Invoke();
                yield break;
            }

            ProcessBuff(buffCardView, hoveredCardView, resetPos);
        }

        private void ProcessBuff(CardView buffCardView, CardView hoveredCardView, Action resetPos)
        {
            var targetCard = hoveredCardView.GetCard();
            
            UnitCard unitCard = targetCard as UnitCard;
            SpecialCard specialCard = buffCardView.GetCard() as SpecialCard;

            switch (buffCardView.GetCard().CardData.CardType)
            {
                case CardType.Healing:
                  //  ApplyHealingBuff(unitCard, specialCard);
                    break;
                case CardType.Buff:
                    ApplyStatBuff(unitCard, specialCard);
                    break;
            }

            hoveredCardView.UpdateView();
            CompleteBuff(buffCardView, resetPos);
        }

        private void ApplyStatBuff(UnitCard unitCard, SpecialCard specialCard)
        {
            if (unitCard == null || specialCard == null) return;

            _buffService.Buff(unitCard, specialCard);
        }

        private void CompleteBuff(CardView buffCardView, Action resetPos)
        {
            _tableService.RemoveCardFromPlayerHand(buffCardView.GetCard());
            Object.Destroy(buffCardView.gameObject);
            resetPos?.Invoke();
        }
    }
}
