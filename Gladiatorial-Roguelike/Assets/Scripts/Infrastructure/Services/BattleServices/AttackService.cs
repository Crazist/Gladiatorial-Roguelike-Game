using System.Collections.Generic;
using Logic.Enteties;
using UI.View;

namespace Infrastructure.Services.BattleServices
{
    public class AttackService
    {
        private readonly List<AttackInfo> _attacks = new List<AttackInfo>();

        public void AddAttack(CardView attacker, CardView defender) =>
            _attacks.Add(new AttackInfo { Attacker = attacker, Defender = defender });

        public void RemoveAttack(CardView attacker) => 
            _attacks.RemoveAll(attack => attack.Attacker == attacker);

        public List<AttackInfo> GetAttacks() =>
            _attacks;
        public void DeactivateAllArrows()
        {
            foreach (var attack in _attacks)
            {
                var attackAndDefence = attack.Attacker.GetAttackAndDefence();
                if (attackAndDefence != null)
                {
                    attackAndDefence.RemoveLine();
                }
            }
        }
        public void ClearAttacks() =>
            _attacks.Clear();
    }
}