using System.Collections.Generic;
using Infrastructure.Services;
using Logic.Types;
using UnityEngine;
using CustomEditorAttributes;

namespace Data.Cards
{
    [CreateAssetMenu(fileName = "New Card", menuName = "Gladiatorial-Roguelike/Card/Create New Card")]
    public class CardData : ScriptableObject
    {
        [ConditionalHide("Category", (int)CardCategory.Unit)]
        [SerializeField] private UnitCardData _unitData;

        [ConditionalHide("Category", (int)CardCategory.Special)]
        [SerializeField] private SpecialCardData _specialData;

        public List<Sprite> RarityIcons;

        public string CardName;
        public CardType CardType;
        public CardRarity CardRarity;
        public CardCategory Category;
        public DeckType DeckType;

        public UnitCardData UnitData => Category == CardCategory.Unit ? _unitData : null;
        public SpecialCardData SpecialData => Category == CardCategory.Special ? _specialData : null;
    }
}