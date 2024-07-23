using System;

namespace Infrastructure.Services.BattleServices
{
    public class TurnService
    {
        public event Action OnPlayerTurnStart;
        public event Action OnEnemyNonPlayerInteractionStateStart;
        public event Action OnTurnEnd;
        
        public void StartPlayerTurn() => 
            OnPlayerTurnStart?.Invoke();

        public void StartNonPlayerInteractionStateStart() => 
            OnEnemyNonPlayerInteractionStateStart?.Invoke();
        public void StartTurnEnd() => 
            OnTurnEnd?.Invoke();
    }
}