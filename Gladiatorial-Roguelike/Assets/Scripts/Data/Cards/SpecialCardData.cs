using System;

namespace Data.Cards
{
    [Serializable]
    public class SpecialCardData
    {
        public int EffectValue;
        public int XpWithUse;
        public int XpThreshold;
        public float XPThresholdMultiplier;
        public string SpecialEffect;
    }
}