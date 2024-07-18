using System;
using System.Collections;
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

        [Inject]
        public void Inject(TableService tableService, CoroutineCustomRunner coroutineCustomRunner)
        {
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
           
            if (hoveredCardView != null)
            {
                var targetCard = hoveredCardView.GetCard();

                if (targetCard.CardData.Category != CardCategory.Unit)
                {
                    resetPos.Invoke();
                    yield break;
                }

                switch (buffCardView.GetCard().CardData.CardType)
                {
                    case CardType.Healing:
                        ApplyHealingBuff(hoveredCardView);
                        break;
                    case CardType.Buff:
                        ApplyStatBuff(hoveredCardView);
                        break;
                }

                _tableService.RemoveCardFromPlayerHand(buffCardView.GetCard());
                Object.Destroy(buffCardView.gameObject);
            }
            else
            {
                resetPos.Invoke();
            }

            buffCardView.ChangeRaycasts(true);
        }

        private void ApplyHealingBuff(CardView targetCardView)
        {
            var targetCard = targetCardView.GetCard();
            if (targetCard is UnitCard unitCard)
            {
                unitCard.Hp += 10;
            }
        }

        private void ApplyStatBuff(CardView targetCardView)
        {
            var targetCard = targetCardView.GetCard();
            if (targetCard is UnitCard unitCard)
            {
                unitCard.Attack += 5;
                unitCard.Defense += 5;
            }
        }
    }
}