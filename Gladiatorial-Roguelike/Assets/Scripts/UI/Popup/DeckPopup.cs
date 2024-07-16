using System.Collections.Generic;
using Logic.Entities;
using UnityEngine;

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
            
            int index = 0;

            foreach (var card in cards)
            {
                CardTextPrefab cardTextObject;
                if (index < _textObjects.Count)
                {
                    cardTextObject = _textObjects[index];
                    cardTextObject.gameObject.SetActive(true);
                }
                else
                {
                    cardTextObject = Instantiate(_cardTextPrefab, _contentArea);
                    _textObjects.Add(cardTextObject);
                }

                cardTextObject.SetCardData(card.CardRarity + " " + card.CardName);
                index++;
            }

            for (int i = index; i < _textObjects.Count; i++)
            {
                _textObjects[i].gameObject.SetActive(false);
            }

            gameObject.SetActive(true);
        }

        public void Hide() => 
            gameObject.SetActive(false);

        private void SetPosition(Vector3 position) =>
            _contentArea.transform.position = position;
    }
}