using UnityEngine;

namespace Logic.Cards
{
    [CreateAssetMenu(fileName = "New Special Card", menuName = "Gladiatorial-Roguelike/Card/Create New Special Card")]
    public class SpecialCardData : CardData
    {
        public int EffectValue;
        public string SpecialEffect;
    }
}