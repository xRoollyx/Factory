using System;
using UnityEngine;

namespace myProject{
    public class GameManager : MonoBehaviour{
        private DiContainer _container;
        private WorldManager _worldManager;
        private WorldVisualManager _worldVisualManager;
        private CameraManager _cameraManager;
        private InputController _inputController;
        private BuildManager _buildManager;
        private WorldGeneration _worldGeneration;
        private ButtonController _buttonController;
        private UiManager _uiManager;
        private BankManager _bankManager;
        
        private GameState _gameState;

        public void Initialize(DiContainer container){
            _container = container;
            _gameState = _container.Resolve<IGameStateProvider>().gameStateOrigin;
            _worldManager = _container.Resolve<WorldManager>();
            _worldVisualManager = _container.Resolve<WorldVisualManager>();
            _cameraManager = _container.Resolve<CameraManager>();
            _inputController = _container.Resolve<InputController>();
            _buildManager = _container.Resolve<BuildManager>();
            _worldGeneration = _container.Resolve<WorldGeneration>();
            _buttonController = _container.Resolve<ButtonController>();
            _uiManager = _container.Resolve<UiManager>();
            _bankManager = _container.Resolve<BankManager>();
            
            
            _worldManager.Initialize(_gameState.worldData, _worldVisualManager, _worldGeneration );
            _bankManager.Initialize(_gameState.moneyData);
            _cameraManager.Initialize(_gameState.cameraData);
            _inputController.Initialize();
            _buildManager.Initialize(_worldManager, _gameState, _uiManager);
            _buttonController.Initialize(_buildManager, _inputController);
            
        }
    }
}