using System;
using System.Collections;
using Infrastructure.Services.BuffsService;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using UnityEngine;
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
        public void Inject(TableService tableService, CoroutineCustomRunner coroutineCustomRunner,
            BuffService buffService)
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

        public void ApplyBuffForAi(CardView buffCardView, CardView unitCardView)
        {
            buffCardView.ChangeRaycasts(false);
            _coroutineCustomRunner.StartCoroutine(ApplyAIBuffCoroutine(buffCardView, unitCardView, null));
        }

        private IEnumerator ApplyBuffCoroutine(CardView buffCardView, Action resetPos)
        {
            yield return null;

            CardView targetCardView = GetHoveredCardView();
            if (targetCardView == null || targetCardView.GetCard().CardData.Category != CardCategory.Unit)
            {
                buffCardView.ChangeRaycasts(true);
                resetPos?.Invoke();
                yield break;
            }

            ProcessBuff(buffCardView, targetCardView, resetPos);
        }

        private IEnumerator ApplyAIBuffCoroutine(CardView buffCardView, CardView targetCardView, Action resetPos)
        {
            yield return null;

            ProcessBuff(buffCardView, targetCardView, resetPos);
        }

        private void ProcessBuff(CardView buffCardView, CardView targetCardView, Action resetPos)
        {
            var targetCard = targetCardView.GetCard();
            var buffCard = buffCardView.GetCard();

            switch (buffCard.CardData.CardType)
            {
                case CardType.Healing:
                    ApplyHealingBuff(targetCard as UnitCard, buffCard as SpecialCard);
                    break;
                case CardType.Buff:
                    ApplyStatBuff(targetCard as UnitCard, buffCard as SpecialCard);
                    break;
            }

            targetCardView.UpdateView();
            CompleteBuff(buffCardView, resetPos);
        }

        private void ApplyHealingBuff(UnitCard unitCard, SpecialCard specialCard)
        {
            if (unitCard == null || specialCard == null) return;
            //  _buffService.Heal(unitCard, specialCard.SpecialEffectValue);
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

        private CardView GetHoveredCardView() => _tableService.GetHoveredCard();
    }
}
