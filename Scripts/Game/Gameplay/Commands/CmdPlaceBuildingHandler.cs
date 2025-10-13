using myProject.Scripts.Game.State.Buildings;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.Root;

namespace myProject.Scripts.Game.Gameplay.Commands{
    public class CmdPlaceBuildingHandler: ICommandHandler<CmdPlaceBuilding>{
        private readonly GameStateProxy _gameState;

        public CmdPlaceBuildingHandler(GameStateProxy  gameState){
            _gameState = gameState;
        }
        public bool Handle(CmdPlaceBuilding command){
            var entityId = _gameState.GetEntityId();
            var newBuildingEntity = new BuildingEntity{
                Id = entityId,
                TypeId = command.BuildingTypeId,
                Position = command.Position,
            };

            var newBuildingEntityProxy = new BuildingEntityProxy(newBuildingEntity);
            _gameState.Buildings.Add(newBuildingEntityProxy);
            
            return true;
        }
    }
}