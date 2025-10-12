using System;
using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.GameRoot;
using myProject.Scripts.Game.MainMenu.Root.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace myProject.Scripts.Game.MainMenu.Root{
    public class MainMenuEnterPoint : MonoBehaviour{
        private  DiContainer _mainMenuContainer;

        [SerializeField] private UIMainMenuBinder _sceneUIRootPrefab;
        
        public event Action GoToGameplayButtonClicked;
       
        public void Run(DiContainer container){
            _mainMenuContainer = container;
            
            var uiScene = Instantiate(_sceneUIRootPrefab);
            _mainMenuContainer.Resolve<UiRootView>().AttachSceneUI(uiScene.gameObject);

            uiScene.GoToGameplayButtonClicked += () => GoToGameplayButtonClicked?.Invoke();
        }
    }
}