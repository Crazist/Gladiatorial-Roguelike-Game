using System;
using Logic.Types;

namespace Infrastructure.Data
{
    [Serializable]
    public class ScoreByComplexity
    {
        public DeckComplexity Complexity;
        public int Score;
    }
}