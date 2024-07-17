using System.Collections.Generic;
using UnityEngine;
using Logic.Entities;

namespace UI.Elements
{
    public class DeckPopup : MonoBehaviour
    {
        [SerializeField] private CardTextPrefab _cardTextDisplayPrefab;
        [SerializeField] private Transform _contentArea;

        private readonly List<CardTextPrefab> _textObjects = new List<CardTextPrefab>();

        private void Start() =>
            gameObject.SetActive(false);

        public void Show(Vector3 position, Dictionary<Card, int> sortedCardData)
        {
            SetPosition(position);
            UpdateCardList(sortedCardData);
            gameObject.SetActive(true);
        }

        public void Hide() =>
            gameObject.SetActive(false);

        private void SetPosition(Vector3 position) =>
            _contentArea.transform.position = position;

        private void UpdateCardList(Dictionary<Card, int> sortedCardData)
        {
            int index = 0;
            foreach (var cardData in sortedCardData)
            {
                CardTextPrefab cardTextObject = GetOrCreateTextObject(index);
                cardTextObject.SetCardData(cardData.Key.CardRarity, $"{cardData.Key.CardName} x{cardData.Value}");
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
                CardTextPrefab newTextObject = Instantiate(_cardTextDisplayPrefab, _contentArea);
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
