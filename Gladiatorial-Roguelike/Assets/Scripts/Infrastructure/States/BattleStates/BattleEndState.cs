using System;
using Infrastructure.StateMachines;
using Zenject;

namespace Infrastructure
{
    public class BattleEndState : IState
    {
        private BattleStateMachine _battleStateMachine;

        [Inject]
        private void Inject(BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

        public void Enter()
        {
             EndBattle();
        }

        public void Exit()
        {
            // ОЧИСТИТЬ ВСЕ СЕРВИСЫ БИТВЫ!!!
            // Очистка состояния
        }

        private void EndBattle()
        {
            // Логика завершения битвы
            // Обработка результатов битвы, награды и т.д.
        }
    }
}