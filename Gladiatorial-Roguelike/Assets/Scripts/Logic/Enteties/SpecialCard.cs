using Logic.Cards;

namespace Logic.Entities
{
    public class SpecialCard : Card
    {
        public string SpecialEffect;
        public int EffectValue;

        public SpecialCard(SpecialCardData data) : base(data)
        {
            SpecialEffect = data.SpecialEffect;
            EffectValue = data.EffectValue;
        }

        public override void InitializeView(DynamicCardView dynamicCardView) => 
            dynamicCardView.Initialize(this);
    }
}