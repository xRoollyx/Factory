using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Commands;
using myProject.Scripts.Game.Gameplay.Root;
using myProject.Scripts.Game.Gameplay.Root.View;
using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.GameRoot;
using myProject.Scripts.Game.MainMenu.Root;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.Providers;
using ObservableCollections;
using R3;
using UnityEngine;

namespace myProject{
    public class GameplayEntryPoint : MonoBehaviour{
        [SerializeField] private UIGameplayRootBinder _sceneUIRootPrefab;
        [SerializeField] private WorldGameplayRootBinder _worldRootBinder;
        
        

        private DiContainer _gameplayContainer;
        public Observable<GameplayExitParams> Run(DiContainer gameplayContainer, GameplayEnterParams enterParams){
            
            GameplayRegistrations.Register(gameplayContainer, enterParams);
            var gameplayViewModelsContainer = new DiContainer(gameplayContainer);
            GameplayViewModelsRegistrations.Register(gameplayViewModelsContainer);
            
            _worldRootBinder.Bind(gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>());

            gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();
            
            
            var uiRoot = gameplayContainer.Resolve<UiRootView>();
            var uiScene = Instantiate(_sceneUIRootPrefab);
            uiRoot.AttachSceneUI(uiScene.gameObject);

            var exitSceneSignalSubj = new  Subject<Unit>();
            
            uiScene.Bind(exitSceneSignalSubj);
            
            Debug.Log($"MapId: " + enterParams.MapId);

            var mainMenuEnterParams = new MainMenuEnterParams("Fatality");
            var exitParams = new GameplayExitParams(mainMenuEnterParams);
            var exitToMainMenuSceneSignal = exitSceneSignalSubj.Select(_ => exitParams);

            return exitToMainMenuSceneSignal;
        }
    }
}