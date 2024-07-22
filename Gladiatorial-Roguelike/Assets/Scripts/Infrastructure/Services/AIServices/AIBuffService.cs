using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Infrastructure.Services.CardsServices;
using Logic.Types;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.AIServices
{
    public class AIBuffService
    {
        private TableService _tableService;
        private CardBuffService _buffService;
        private System.Random _random;

        [Inject]
        public void Inject(TableService tableService,
            CardBuffService buffService)
        {
            _tableService = tableService;
            _buffService = buffService;
            _random = new System.Random();
        }

        public IEnumerator ExecuteBuffsRoutine()
        {
            yield return ApplyBuffsRoutine();
        }

        private IEnumerator ApplyBuffsRoutine()
        {
            yield return new WaitForSeconds(1);

            var enemyHand = _tableService.GetEnemyHandViews();
            var tableViews = _tableService.GetEnemyTableViews();
            
            var targetCardView = GetRandomUnitCardOnTable(tableViews);

            if (targetCardView == null)
                yield break;


            var buffCards = new List<CardView>();

            foreach (var card in enemyHand)
            {
                if (card.GetCard().CardData.Category == CardCategory.Special && _random.NextDouble() < 1)
                {
                    buffCards.Add(card);
                }
            }

            foreach (var buffCard in buffCards)
            {
                _tableService.GetEnemyHandViews().Remove(buffCard);
                yield return ApplyBuffWithDelay(buffCard, targetCardView);
            }
        }

        private CardView GetRandomUnitCardOnTable(List<CardView> enemyCardViews)
        {
            List<CardView> unitCardViews =
                enemyCardViews.FindAll(cv => cv.GetCard().CardData.Category == CardCategory.Unit);

            if (unitCardViews.Count == 0) return null;

            int randomIndex = _random.Next(unitCardViews.Count);
            return unitCardViews[randomIndex];
        }

        private IEnumerator ApplyBuffWithDelay(CardView buffCard, CardView targetCardView)
        {
            buffCard.GetCardDisplay().FlipCard();

            var moveTween = buffCard.GetRectTransform().DOMove(targetCardView.GetRectTransform().position, 0.5f);
            yield return moveTween.WaitForCompletion();

            moveTween.Kill();

            _tableService.GetEnemyHandViews().Remove(buffCard);

            _buffService.ApplyBuffForAi(buffCard, targetCardView);

            yield return new WaitForSeconds(1);
        }
    }
}