using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services.BuffsService;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using Zenject;

namespace Infrastructure.Services.CardsServices
{
    public class CardBuffService
    {
        private TableService _tableService;
        private CoroutineCustomRunner _coroutineCustomRunner;
        private BuffProcessingService _buffProcessingService;

        [Inject]
        public void Inject(TableService tableService, CoroutineCustomRunner coroutineCustomRunner,
            BuffProcessingService buffProcessingService)
        {
            _buffProcessingService = buffProcessingService;
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
            CardType buffCardType = buffCardView.GetCard().CardData.CardType;

            if (!IsValidBuffTarget(targetCardView, buffCardType))
            {
                buffCardView.ChangeRaycasts(true);
                resetPos?.Invoke();
                yield break;
            }

            _buffProcessingService.ProcessBuff(buffCardView, targetCardView, resetPos);
        }

        private IEnumerator ApplyAIBuffCoroutine(CardView buffCardView, CardView targetCardView, Action resetPos)
        {
            yield return null;

            _buffProcessingService.ProcessBuff(buffCardView, targetCardView, resetPos);
        }

        private bool IsValidBuffTarget(CardView targetCardView, CardType buffCardType)
        {
            if (targetCardView == null) return false;
            
            List<Card> table = buffCardType == CardType.Recruit ? _tableService.GetEnemyTable() : _tableService.GetPlayerTable();
            return table.Contains(targetCardView.GetCard());
        }

        private CardView GetHoveredCardView() => _tableService.GetHoveredCard();
    }
}
