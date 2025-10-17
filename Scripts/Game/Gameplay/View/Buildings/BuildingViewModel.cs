using System.Collections.Generic;
using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.Settings.Gameplay.Buildings;
using myProject.Scripts.Game.State.Buildings;
using R3;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.View.Buildings{
    public class BuildingViewModel{
        private readonly BuildingEntityProxy _buildingEntity;
        private readonly BuildingSettings _buildingSettings;
        private readonly BuildingsService _buildingsService;
        private readonly Dictionary<int, BuildingLevelSettings> _levelSettingsMap =  new ();

        public readonly int BuildingEntityId;
        public readonly string TypeId;
        
        public ReadOnlyReactiveProperty<Vector3Int> Position{ get; }

        public BuildingViewModel(
            BuildingEntityProxy buildingEntity, 
            BuildingSettings buildingSettings, 
            BuildingsService buildingsService){
            
            BuildingEntityId = buildingEntity.Id;
            TypeId = buildingSettings.TypeId;
            
            _buildingEntity = buildingEntity;
            _buildingSettings = buildingSettings;
            _buildingsService = buildingsService;

            foreach (var levelSetting in _buildingSettings.LevelSettings){
                _levelSettingsMap[levelSetting.Level] =  levelSetting;
            }
            
            Position = buildingEntity.Position;
        }
        
        public BuildingLevelSettings GetLevelSettings(int level){
            return _levelSettingsMap[level];
        }
    }
}