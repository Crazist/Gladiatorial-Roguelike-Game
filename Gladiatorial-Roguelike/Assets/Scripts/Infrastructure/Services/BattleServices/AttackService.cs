using System.Collections.Generic;
using Logic.Enteties;
using UI.Elements;
using UI.View;

namespace Infrastructure.Services.BattleServices
{
    public class AttackService
    {
        private readonly List<AttackInfo> _attacks = new List<AttackInfo>();

        public void AddAttack(CardView attacker, CardView defender) =>
            _attacks.Add(new AttackInfo { Attacker = attacker, Defender = defender });

        public List<AttackInfo> GetAttacks() =>
            _attacks;

        public void ClearAttacks() =>
            _attacks.Clear();
    }
}