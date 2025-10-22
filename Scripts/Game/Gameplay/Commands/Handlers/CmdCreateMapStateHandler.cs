using System.Collections.Generic;
using System.Linq;
using myProject.Scripts.Game.Settings;
using myProject.Scripts.Game.State.Buildings;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.Maps;
using myProject.Scripts.Game.State.Root;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.Commands{
    public class CmdCreateMapStateHandler: ICommandHandler<CmdCreateMapState>{
        private readonly GameStateProxy _gameState;
        private readonly GameSettings _gameSettings;

        public CmdCreateMapStateHandler(GameStateProxy gameState, GameSettings gameSettings){
            _gameState = gameState;
            _gameSettings = gameSettings;
        }

        public bool Handle(CmdCreateMapState command){
            var isMapAlredyExisted = _gameState.Maps.Any(m => m.Id ==  command.MapId);

            if (isMapAlredyExisted){
                Debug.LogError($"Map with Id = {command.MapId} already exists!");
                return false;
            }
            
            var newMapSettings = _gameSettings.MapsSettings.Maps.First(m => m.MapId == command.MapId);
            var newMapInitialStateSettings = newMapSettings.InitializeStateSettings;

            var initialBuildings = new List<BuildingEntity>();
            foreach (var buildingSettings in newMapInitialStateSettings.Buildings){
                var initialBuilding = new BuildingEntity{
                    Id = _gameState.CreateEntityId(),
                    TypeId = buildingSettings.TypeId,
                    Position = buildingSettings.Position,
                    Level = buildingSettings.Level,
                };
                initialBuildings.Add(initialBuilding);
            }

            var newMapState = new MapState{
                Id = command.MapId,
                Buildings = initialBuildings,
            };

            var newMap = new Map(newMapState);
            
            _gameState.Maps.Add(newMap);
            
            return true;
        }
    }
}