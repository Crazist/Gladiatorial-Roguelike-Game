using UI.Model;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class DeckChooseWindow : WindowBase
    {
        [SerializeField] private Button _playersDeck;
       
        private PermaDeckModel _permaDeckModel;

        [Inject]
        private void Inject(PermaDeckModel permaDeckModel)
        {
            _permaDeckModel = permaDeckModel;

            RegisterBtn();
        }

        private void RegisterBtn() => 
            _playersDeck.onClick.AddListener(SetPermaDeckContinueBtn);

        private void SetPermaDeckContinueBtn() => 
            _permaDeckModel.SetHasContinueBtn(false);
    }
}