using System.Collections.Generic;
using Logic.Enteties;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Rewards", menuName = "Rewards/Rewards")]
    public class Rewards : ScriptableObject
    {
        public List<RewardRange> Ranges;
    }
}