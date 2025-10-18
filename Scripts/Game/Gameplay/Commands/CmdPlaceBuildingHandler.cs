using System.Linq;
using myProject.Scripts.Game.State.Buildings;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.Root;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.Commands{
    public class CmdPlaceBuildingHandler: ICommandHandler<CmdPlaceBuilding>{
        private readonly GameStateProxy _gameState;

        public CmdPlaceBuildingHandler(GameStateProxy  gameState){
            _gameState = gameState;
        }
        public bool Handle(CmdPlaceBuilding command){
            var currentMap = _gameState.Maps.FirstOrDefault(m => m.Id == _gameState.CurrentMapId.CurrentValue);
            if (currentMap == null){
                Debug.LogError($"Could not find current map for id {_gameState.CurrentMapId.CurrentValue}");
            }
            
            var entityId = _gameState.CreateEntityId();
            var newBuildingEntity = new BuildingEntity{
                Id = entityId,
                TypeId = command.BuildingTypeId,
                Position = command.Position,
            };

            var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);

            if (currentMap != null) currentMap.Buildings.Add(newBuildingEntityProxy);

            return true;
        }
    }
}