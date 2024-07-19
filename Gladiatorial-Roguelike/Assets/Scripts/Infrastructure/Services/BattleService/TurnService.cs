using System;

namespace Infrastructure.Services.BattleService
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