using System;

namespace Infrastructure.Services.BattleServices
{
    public class TurnService
    {
        public event Action OnPlayerTurnStart;
        public event Action OnEnemyNonPlayerInteractionStateStart;
        
        public void StartPlayerTurn() => 
            OnPlayerTurnStart?.Invoke();

        public void StartNonPlayerInteractionStateStart() => 
            OnEnemyNonPlayerInteractionStateStart?.Invoke();
    }
}