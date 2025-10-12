using System;
using myProject.Scripts.Game.MainMenu.Root.View;
using UnityEngine;

namespace myProject{
    public class MainMenuEnterPoint : MonoBehaviour{
        private  DiContainer _mainMenuContainer;
        
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;
        
        public event Action GoToGameplaySceneRequested;
        
        public void Run(DiContainer container){
            _mainMenuContainer = container;
            
            var uiScene = Instantiate(_sceneUIRootPrefab);
            container.Resolve<UiRootView>().AttachSceneUI(uiScene.gameObject);
            
            uiScene.GoToGameplayButtonClicked += () => GoToGameplaySceneRequested?.Invoke();
        }
    }
}