using UnityEngine;

namespace Data.Cards
{
    [CreateAssetMenu(fileName = "LevelMultiplierConfig", menuName = "Configs/LevelMultiplierConfig", order = 1)]
    public class LevelMultiplierConfig : ScriptableObject
    {
        public LevelMultiplier[] LevelMultipliers;

        public float GetMultiplierForLevel(int level)
        {
            foreach (var levelMultiplier in LevelMultipliers)
            {
                if (levelMultiplier.Level == level)
                {
                    return levelMultiplier.Multiplier;
                }
            }
            
            return 1f;
        }
    }
}