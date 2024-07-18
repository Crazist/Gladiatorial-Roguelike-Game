using Infrastructure;
using Infrastructure.Services;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class MenuWindow : WindowBase
    {
        [SerializeField] private Button _newRun;
       
        private PersistentProgressService _persistentProgress;
        private EnemyService _enemyService;

        [Inject]
        private void Inject(PersistentProgressService persistentProgress, EnemyService enemyService)
        {
            _enemyService = enemyService;
            _persistentProgress = persistentProgress;

            RegisterBtn();
        }

        private void RegisterBtn() => 
            _newRun.onClick.AddListener(RefreshPersistentData);

        private void RefreshPersistentData()
        {
            _persistentProgress.PlayerProgress.ClearProgress();
            _enemyService.InitEnemyDecks();
        }
    }
}