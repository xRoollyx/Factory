using System.Linq;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.GameResources;
using myProject.Scripts.Game.State.Root;

namespace myProject.Scripts.Game.Gameplay.Commands{
    public class CmdResourcesAddHandler: ICommandHandler<CmdResourcesAdd>{
        
        private readonly GameStateProxy  _gameState;

        public CmdResourcesAddHandler(GameStateProxy gameState){
            _gameState = gameState;
        }

        public bool Handle(CmdResourcesAdd command){
            var requiredResourceType = command.ResourceType;
            var requiredResource = _gameState.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);
            if (requiredResource == null){
                requiredResource = CreateNewResource(requiredResourceType);
            }
            
            requiredResource.Amount.Value += command.Amount;
            return true;
        }

        private Resource CreateNewResource(ResourceType requiredResourceType){
            var newResourceData = new ResourceData{
                ResourceType = requiredResourceType,
                Amount = 0
            };
            
            var newResource = new Resource(newResourceData);
            _gameState.Resources.Add(newResource);
            return newResource;
        }
    }
}