using System;
using Logic.Cards;
using Logic.Types;

namespace Logic.Entities
{
    [Serializable]
    public class SpecialCard : Card
    {
        public string SpecialEffect;
        public int EffectValue;

        public SpecialCard(SpecialCardData data) : base(data)
        {
            SpecialEffect = data.SpecialEffect;
            EffectValue = data.EffectValue;
        }
    }
}