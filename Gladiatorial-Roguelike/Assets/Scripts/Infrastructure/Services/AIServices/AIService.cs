using System.Collections;
using System.Collections.Generic;
using Infrastructure.StateMachines;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services
{
    public class AIService
    {
        private List<EnemyCardDropArea> _enemyDropAreas;

        private TableService _tableService;
        private System.Random _random;

        [Inject]
        public void Inject(TableService tableService)
        {
            _tableService = tableService;
            _random = new System.Random();
        }

        public void Initialize(List<EnemyCardDropArea> enemyDropAreas) =>
            _enemyDropAreas = enemyDropAreas;

        public IEnumerator ExecuteEnemyTurnRoutine()
        {
            yield return new WaitForSeconds(1);
            yield return PlayEnemyCardsWithDelay();
            yield return new WaitForSeconds(1);
        }

        private IEnumerator PlayEnemyCardsWithDelay()
        {
            List<CardView> enemyHand = _tableService.GetEnemyHandViews();
            List<Card> cardsToPlay = new List<Card>();

            foreach (var cardToPlay in enemyHand)
            {
                Card card = cardToPlay.GetCard();

                if (card.CardData.Category == CardCategory.Unit && _random.NextDouble() < 0.85f)
                {
                    var availableDropArea = GetAvailableDropArea();

                    if (availableDropArea != null)
                    {
                        cardsToPlay.Add(card);
                        yield return PlaceCardInDropAreaWithDelay(enemyHand, cardToPlay, availableDropArea);
                    }
                }
            }

            if (cardsToPlay.Count == 0)
            {
                var cardToPlay = TryPlayAtLeastOneCard();
                if (cardToPlay != null)
                {
                    var availableDropArea = GetAvailableDropArea();
                    if (availableDropArea != null)
                    {
                        _tableService.GetEnemyHandViews().Remove(cardToPlay);
                        _tableService.GetEnemyTableViews().Add(cardToPlay);
                        yield return PlaceCardInDropAreaWithDelay(enemyHand, cardToPlay, availableDropArea);
                    }
                }
            }
        }

        private CardView TryPlayAtLeastOneCard()
        {
            var enemyHand = _tableService.GetEnemyHandViews();
            foreach (var cardToPlay in enemyHand)
            {
                if (cardToPlay.GetCard().CardData.Category == CardCategory.Unit)
                {
                    return cardToPlay;
                }
            }

            return null;
        }

        private IEnumerator PlaceCardInDropAreaWithDelay( List<CardView> enemyCardViews, CardView cardToPlay, EnemyCardDropArea dropArea)
        {
            var cardView = enemyCardViews.Find(cv => cv.GetCard() == cardToPlay.GetCard());
            
            if (cardView != null)
            {
                cardView.ChangeRaycasts(false);
                cardView.GetCardDisplay().FlipCard();
                dropArea.HandleDrop(cardView, null);

                yield return new WaitForSeconds(1);
            }
        }

        private EnemyCardDropArea GetAvailableDropArea()
        {
            foreach (var dropArea in _enemyDropAreas)
            {
                if (!dropArea.IsOccupied())
                {
                    return dropArea;
                }
            }

            return null;
        }
    }
}