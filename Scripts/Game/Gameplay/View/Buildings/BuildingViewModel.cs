using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.State.Buildings;
using R3;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.View.Buildings{
    public class BuildingViewModel{
        private readonly BuildingEntityProxy _buildingEntity;
        private readonly BuildingsService _buildingsService;

        public readonly int BuildingEntityId;
        
        public ReadOnlyReactiveProperty<Vector3Int> Position{ get; }

        public BuildingViewModel(BuildingEntityProxy buildingEntity, BuildingsService buildingsService){
            BuildingEntityId = buildingEntity.Id;
            
            _buildingEntity = buildingEntity;
            _buildingsService = buildingsService;
            
            Position = _buildingEntity.Position;
        }
    }
}