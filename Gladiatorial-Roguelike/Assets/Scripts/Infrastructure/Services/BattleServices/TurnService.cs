using System;

namespace Infrastructure.Services.BattleServices
{
    public class TurnService
    {
        public event Action OnPlayerTurnStart;
        public event Action OnEnemyTurnStart;

        public void StartPlayerTurn() => 
            OnPlayerTurnStart?.Invoke();

        public void StartEnemyTurn() => 
            OnEnemyTurnStart?.Invoke();
    }
}