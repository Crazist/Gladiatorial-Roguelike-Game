using System.Collections;
using System.Collections.Generic;
using Infrastructure.Services.BattleServices;
using Logic.Entities;
using Logic.Types;
using UI.Elements;
using UI.Elements.CardDrops;
using UI.View;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.AIServices
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

            for (int i = 0; i < enemyHand.Count; i++)
            {
                Card card = enemyHand[i].GetCard();

                if (card.CardData.Category == CardCategory.Unit && _random.NextDouble() < 0.85f)
                {
                    var availableDropArea = GetAvailableDropArea();

                    if (availableDropArea != null)
                    {
                        cardsToPlay.Add(card);
                        yield return PlaceCardInDropAreaWithDelay(enemyHand[i], availableDropArea);
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
                        yield return PlaceCardInDropAreaWithDelay(cardToPlay, availableDropArea);
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

        private IEnumerator PlaceCardInDropAreaWithDelay(CardView cardToPlay, EnemyCardDropArea dropArea)
        {
            cardToPlay.ChangeRaycasts(false);
            cardToPlay.GetCardDisplay().FlipCard();
            dropArea.HandleDrop(cardToPlay, null);

            yield return new WaitForSeconds(1);
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