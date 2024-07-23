using UI.Factory;
using Zenject;

namespace Infrastructure.Services.BattleServices
{
    public class BattleService
    {
        private AttackService _attackService;
        private UIFactory _uiFactory;

        [Inject]
        private void Inject(AttackService attackService, UIFactory uiFactory)
        {
            _attackService = attackService;
            _uiFactory = uiFactory;
        }

        public void VisualizeAttacks()
        {
            foreach (var attack in _attackService.GetAttacks())
            {
                var arrow = _uiFactory.CreateAttackArrow();
                arrow.SetPositions(attack.Attacker.transform.position, attack.Defender.transform.position);
            }
        }

        public void ResolveAttacks()
        {
            foreach (var attack in _attackService.GetAttacks())
            {
                // Логика разрешения атак
            }

            _attackService.ClearAttacks();
        }
    }
}