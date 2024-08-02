using Logic.Enteties;

namespace Infrastructure.Services.BuffsService
{
    public class BuffService
    {
        public void Buff(UnitCard unitCard, SpecialCard specialCard)
        {
            unitCard.Hp += specialCard.EffectValue;
        }
    }
}