using System.Collections.Generic;
using UnityEngine;
using Logic.Entities;

namespace UI.Elements
{
    public class DeckPopup : MonoBehaviour
    {
        [SerializeField] private CardTextPrefab _cardTextPrefab;
        [SerializeField] private Transform _contentArea;

        private readonly List<CardTextPrefab> _textObjects = new List<CardTextPrefab>();

        private void Start() =>
            gameObject.SetActive(false);

        public void Show(Vector3 position, List<Card> cards)
        {
            SetPosition(position);
            UpdateCardList(cards);
            gameObject.SetActive(true);
        }

        public void Hide() =>
            gameObject.SetActive(false);

        private void SetPosition(Vector3 position) =>
            _contentArea.transform.position = position;

        private void UpdateCardList(List<Card> cards)
        {
            int index = 0;
            foreach (var card in cards)
            {
                CardTextPrefab cardTextObject = GetOrCreateTextObject(index);
                cardTextObject.SetCardData($"{card.CardRarity} {card.CardName}");
                index++;
            }

            HideUnusedTextObjects(index);
        }

        private CardTextPrefab GetOrCreateTextObject(int index)
        {
            if (index < _textObjects.Count)
            {
                _textObjects[index].gameObject.SetActive(true);
                return _textObjects[index];
            }
            else
            {
                CardTextPrefab newTextObject = Instantiate(_cardTextPrefab, _contentArea);
                _textObjects.Add(newTextObject);
                return newTextObject;
            }
        }

        private void HideUnusedTextObjects(int usedCount)
        {
            for (int i = usedCount; i < _textObjects.Count; i++)
            {
                _textObjects[i].gameObject.SetActive(false);
            }
        }
    }
}