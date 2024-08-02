using System.Collections.Generic;
using Logic.Enteties;
using Logic.Types;

namespace Infrastructure.Services.BattleServices
{
    public class BattleResultService
    {
        public List<Card> PlayerLost = new();
        public BattleResult BattleResult;
        public int CurrencyReward;
        public Card RewardedCard;
    }
}