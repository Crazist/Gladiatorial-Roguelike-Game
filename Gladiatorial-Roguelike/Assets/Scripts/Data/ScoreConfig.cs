using System.Collections.Generic;
using Infrastructure.Data;
using Logic.Types;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "ScoreConfig", menuName = "Settings/Score Config")]
    public class ScoreConfig : ScriptableObject
    {
        public List<ScoreByComplexity> Scores;

        public int GetScoreForComplexity(DeckComplexity complexity)
        {
            var scoreData = Scores.Find(s => s.Complexity == complexity);
            return scoreData?.Score ?? 0;
        }
    }
}