using System;
using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Root.View;
using myProject.Scripts.Game.GameRoot;
using myProject.Scripts.Game.MainMenu.Root.View;
using UnityEngine;

namespace myProject{
    public class MainMenuEnterPoint : MonoBehaviour{
        public event Action GoToGameplay;
        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;
        
        private  DiContainer _mainMenuContainer;
        public void Run(DiContainer container){
            _mainMenuContainer = container;
            
            var uiMainMenu = Instantiate(_sceneUIRootPrefab);
            _mainMenuContainer.Resolve<UiRootView>().AttachSceneUI(uiMainMenu.gameObject);

            uiMainMenu.GoToGameplayButtonClicked += () => {
                GoToGameplay?.Invoke();
            };

        }
        
       
    }
}