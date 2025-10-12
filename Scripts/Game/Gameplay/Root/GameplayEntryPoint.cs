using UnityEngine;

namespace myProject{
    public class GameplayEntryPoint : MonoBehaviour {

        private DiContainer _gameplayContainer;
        public void Run(DiContainer container){
            _gameplayContainer = container;
            
            var gameManager = FindFirstObjectByType<GameManager>();
            _gameplayContainer.RegisterInstance(gameManager);

            var worldManager = FindFirstObjectByType<WorldManager>(); //???
            _gameplayContainer.RegisterInstance(worldManager);
            
            var worldVisualManager = FindFirstObjectByType<WorldVisualManager>();//???
            _gameplayContainer.RegisterInstance(worldVisualManager);
            
            var cameraManager = FindFirstObjectByType<CameraManager>();
            _gameplayContainer.RegisterInstance(cameraManager);
            
            var inputController = FindFirstObjectByType<InputController>();
            _gameplayContainer.RegisterInstance(inputController);
            
            var buttonController = FindFirstObjectByType<ButtonController>();
            _gameplayContainer.RegisterInstance(buttonController);
            
            var buildManager = FindFirstObjectByType<BuildManager>();
            _gameplayContainer.RegisterInstance(buildManager);
            
            var worldGeneration = FindFirstObjectByType<WorldGeneration>();//???
            _gameplayContainer.RegisterInstance(worldGeneration);
            
            var uiManager = FindFirstObjectByType<UiManager>();
            _gameplayContainer.RegisterInstance(uiManager);
            
            var bankManager = FindFirstObjectByType<BankManager>();
            _gameplayContainer.RegisterInstance(bankManager);
            
            Initialize();
        }

        private void Update(){
            if (Input.GetKeyDown(KeyCode.Space)){
                var gameStateProvider = _gameplayContainer.Resolve<IGameStateProvider>();
                
                gameStateProvider.SaveGameState();
            }
        }

        private void Initialize(){
            _gameplayContainer.Resolve<GameManager>().Initialize(_gameplayContainer);
        }
    }
}