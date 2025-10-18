using System;
using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Common;
using myProject.Scripts.Game.Gameplay.Root;
using myProject.Scripts.Game.GameRoot;
using myProject.Scripts.Game.MainMenu.Root.View;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;


namespace myProject.Scripts.Game.MainMenu.Root{
    public class MainMenuEnterPoint : MonoBehaviour{
        

        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;
        
        
       
        public Observable<MainMenuExitParams> Run(DiContainer mainMenuContainer, MainMenuEnterParams enterParams){
            
            MainMenuRegistrations.Register(mainMenuContainer, enterParams);
            var mainMenuViewModelsContainer = new DiContainer(mainMenuContainer);
            MainMenuViewModelsRegistrations.Register(mainMenuViewModelsContainer);
            
            // для теста
            mainMenuViewModelsContainer.Resolve<UIMainMenuRootViewModel>();

            var uiRoot = mainMenuContainer.Resolve<UiRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);
            
            var exitSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSignalSubj);
            
            Debug.Log($"Main Menu Enter Point {enterParams?.Result}");

            var gameplayEnterParams = new GameplayEnterParams(0);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitToGameplaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);

            return exitToGameplaySceneSignal;
        }
    }
}