using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Root;
using myProject.Scripts.Game.Gameplay.Root.View;
using myProject.Scripts.Game.GameRoot;
using myProject.Scripts.Game.MainMenu.Root;
using R3;
using UnityEngine;

namespace myProject{
    public class GameplayEntryPoint : MonoBehaviour{
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        
        

        private DiContainer _gameplayContainer;
        public Observable<GameplayExitParams> Run(DiContainer gameplayContainer, GameplayEnterParams enterParams){
            
            GameplayRegistrations.Register(gameplayContainer, enterParams);
            var gameplayViewModelsContainer = new DiContainer(gameplayContainer);
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);
            
            //для теста
            gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();
            gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>();
            

            var uiRoot = gameplayContainer.Resolve<UiRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubj = new  Subject<Unit>();
            
            uiScene.Bind(exitSceneSignalSubj);
            
            Debug.Log(enterParams.LevelNumber +"   " + enterParams.SaveFileName);

            var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);
            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

            return exitToMainMenuSceneSignal;
        }
    }
}