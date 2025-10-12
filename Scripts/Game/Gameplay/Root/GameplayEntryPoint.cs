using System;
using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Root.View;
using myProject.Scripts.Game.GameRoot;
using UnityEngine;

namespace myProject{
    public class GameplayEntryPoint : MonoBehaviour {

        [SerializeField] private UIGameplayRootBinder uiGameplayPrefab;
        public event Action GoToMainMenu;
        
        private DiContainer _gameplayContainer;
        public void Run(DiContainer container){
            _gameplayContainer = container;

            var uiGameplay = Instantiate(uiGameplayPrefab);
            _gameplayContainer.Resolve<UiRootView>().AttachSceneUI(uiGameplay.gameObject);
            
            uiGameplay.GoToMainMenuButtonClicked += () => GoToMainMenu?.Invoke();
        }
    }
}