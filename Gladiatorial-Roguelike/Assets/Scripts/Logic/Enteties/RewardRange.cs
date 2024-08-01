using System;
using Logic.Types;

namespace Logic.Enteties
{
    [Serializable]
    public class RewardRange
    {
        public DeckComplexity Complexity;
        public int Min;
        public int Max;
    }
}