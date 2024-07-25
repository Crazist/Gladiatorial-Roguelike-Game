using System;
using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services.BattleServices;
using Infrastructure.Services.BuffsService;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using UI.View;
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

            _buffProcessingService.ProcessBuff(buffCardView, targetCardView, resetPos, true);
        }

        private IEnumerator ApplyAIBuffCoroutine(CardView buffCardView, CardView targetCardView, Action resetPos)
        {
            yield return null;

            _buffProcessingService.ProcessBuff(buffCardView, targetCardView, resetPos, false);
        }

        private bool IsValidBuffTarget(CardView targetCardView, CardType buffCardType)
        {
            if (targetCardView == null) return false;
            
            List<CardView> table = buffCardType == CardType.Recruit ? _tableService.GetEnemyTableViews() : _tableService.GetPlayerTableViews();
            return table.Contains(targetCardView);
        }

        private CardView GetHoveredCardView() => _tableService.GetHoveredCard();
    }
}
