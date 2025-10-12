using System.Collections.Generic;
using System.Linq;
using R3;
using UnityEngine;

namespace myProject{
    public class BuildManager : MonoBehaviour{
        [SerializeField] private BuildingVisualManager buildingVisualManager;
        [SerializeField] private BuildingFactory buildingFactory;
        [SerializeField] private List<BuildingsSo> buildingsSo;

        private WorldManager _worldManager;
        private UiManager _uiManager;
        private GameState _gameState;
        private readonly Dictionary<int, BaseBuilding> _buildingsMap = new();
        
        
        private Vector2Int _currentMousePosition;
        private BaseBuilding _selectedBuild;
        private bool _isBuildMode;
        

        public void Initialize(WorldManager worldManager, GameState gameState, UiManager uiManager){
            _worldManager = worldManager;
            _uiManager = uiManager;
            _gameState = gameState;
            
            CreateBuildingsToData(gameState);
        }
        
        private void OnEnable(){
            InputController.OnLeftClick += LeftClick;
            InputController.OnRightClick += RightClick;
            InputController.OnMouseChangePosition += SetCurrentPosition;
        }

        private void OnDisable(){
            InputController.OnLeftClick -= LeftClick;
            InputController.OnRightClick -= RightClick;
            InputController.OnMouseChangePosition -= SetCurrentPosition;
        }

        private void SetCurrentPosition(Vector2Int position){
            _currentMousePosition = position;
            if (_isBuildMode){
                _selectedBuild.position.Value = _currentMousePosition;
                SelectColorBuilding();
            }
        }

        private void LeftClick(Vector2Int click){
            if (_isBuildMode && _selectedBuild != null && _worldManager.CheckPossibilityBuildings(_selectedBuild)){
                _worldManager.CreateBuildingsToTile(_selectedBuild);
                BuildBuilding(_selectedBuild);
                _selectedBuild.color.Value = Color.white;
                SaveBuildToData(_selectedBuild);
                ExitBuildMode();
            }
        }

        private void SaveBuildToData(BaseBuilding building){
            var data = new BuildingData{
                buildId = building.buildId,
                buildingType = building.buildingsSo.buildingsType,
                position = building.position.Value
            };
            building.data = data;
            _gameState.buildings.Add(data);
        }

        private void RightClick(Vector2Int click){
            if (!_isBuildMode){
                Tile tile = _worldManager.GetTileToWorldCoordinate(_currentMousePosition);
                if (!tile.isEmpty){
                    tile.Interact();
                }
                else{
                    CloseBuildingMenu();
                }
            }
        }
       

        private void Update(){
            if (_buildingsMap.Count > 0){
                foreach (var baseBuilding in _buildingsMap){
                    baseBuilding.Value.Update(Time.deltaTime);
                }
            }
        }
       
        public void BuildBuilding(BaseBuilding building){
           _buildingsMap.Add(building.buildId,building);
        }


        public BaseBuilding GetBuilding(GameObject buildGo){
            return null;
        }

        public void DestroyBuilding(BaseBuilding building){
            _gameState.buildings.Remove(building.data);
            _buildingsMap.Remove(building.buildId);
        }
        
        public void CreateNewBuilding (int buildingIndex){
            var buildingsType = (BuildingsType)buildingIndex;
            
            BaseBuilding building = buildingFactory.CreateNewBuilding(buildingsType, this);
            building.destroyBuilding.Subscribe(unit => DestroyBuilding(building));
            
            if (_selectedBuild != null){
                return;
            }
                
            _selectedBuild = building;
            SetCurrentPosition(_currentMousePosition);
            _isBuildMode = true;
        }

        private void CreateBuildingsToData(GameState gameState){
            foreach (var buildingData in gameState.buildings){
                var building = buildingFactory.CreateBuilding(buildingData.buildingType, this, buildingData.buildId);
                building.position.Value = buildingData.position;
                _worldManager.CreateBuildingsToTile(building);
                building.color.Value = Color.white;
                building.data = buildingData;
                BuildBuilding(building);
            }
        }
        
        private void ExitBuildMode(){
            _isBuildMode = false;
            _selectedBuild = null;
        }

        public void OpenBuildingMenu(ReactiveProperty<int> amount, ReactiveProperty<ItemsType> itemType, BaseBuilding building){
            _uiManager.ShowBuildMenu(amount, itemType, building);
        }
        
        public void CloseBuildingMenu(){
            _uiManager.HideBuildMenu();
        }

        private void SelectColorBuilding(){
           if(_worldManager.CheckPossibilityBuildings(_selectedBuild)){
               _selectedBuild.color.Value = Color.green;
           }
           else{
               _selectedBuild.color.Value = Color.red;
           }
        }

        public int GetBuildId(){
            return _gameState.CreateEntityId();
        }

       
    }
}