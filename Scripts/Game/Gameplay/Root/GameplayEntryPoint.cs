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
            
            //для теста
            var gameStateProvider = gameplayContainer.Resolve<IGameStateProvider>();

            gameStateProvider.GameState.Buildings.ObserveAdd().Subscribe(e => {
                var building = e.Value;
                Debug.Log("Building placed: " 
                          + building.TypeId + "Id: "
                          + building.Id + "Position: "
                          + building.Position);
            });
            
            var buildingsService = gameplayContainer.Resolve<BuildingsService>();
            buildingsService.PlaceBuilding("Vasan", new Vector3Int(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
            
            // end
            
            _worldRootBinder.Bind(gameplayViewModelsContainer.Resolve<WorldGameplayRootViewModel>());

            gameplayViewModelsContainer.Resolve<UIGameplayRootViewModel>();
            
            
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