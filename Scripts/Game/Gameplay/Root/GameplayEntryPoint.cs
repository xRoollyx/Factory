using System;
using myProject.Scripts.Game.Gameplay.Root.View;
using UnityEngine;

namespace myProject{
    public class GameplayEntryPoint : MonoBehaviour{

        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        
        public event Action GoToMainMenuSceneRequested;

        private DiContainer _gameplayContainer;
        public void Run(DiContainer container){
            _gameplayContainer = container;

            var uiScene = Instantiate(_sceneUIRootPrefab);
            container.Resolve<UiRootView>().AttachSceneUI(uiScene.gameObject);
            
            uiScene.GoToMainMenuButtonClicked += () => GoToMainMenuSceneRequested?.Invoke();
            
        }
    }
}