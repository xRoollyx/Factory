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
        private  DiContainer _mainMenuContainer;

        [SerializeField] private UIMainMenuRootBinder _sceneUIRootPrefab;
        
        
       
        public Observable<MainMenuExitParams> Run(DiContainer container, MainMenuEnterParams enterParams){
            _mainMenuContainer = container;
            
            var uiScene = Instantiate(_sceneUIRootPrefab);
            _mainMenuContainer.Resolve<UiRootView>().AttachSceneUI(uiScene.gameObject);
            
            var exitSignalSubj = new Subject<Unit>();
            uiScene.Bind(exitSignalSubj);
            
            Debug.Log($"Main Menu Enter Point {enterParams?.Result}");

            var saveFileName = "first.save";
            var levelNumber = Random.Range(0, 3);
            var gameplayEnterParams = new GameplayEnterParams(saveFileName, levelNumber);
            var mainMenuExitParams = new MainMenuExitParams(gameplayEnterParams);
            var exitToGameplaySceneSignal = exitSignalSubj.Select(_ => mainMenuExitParams);

            return exitToGameplaySceneSignal;
        }
    }
}