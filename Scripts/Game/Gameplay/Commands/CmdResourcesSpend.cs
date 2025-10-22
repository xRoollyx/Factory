using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.GameResources;

namespace myProject.Scripts.Game.Gameplay.Commands{
    public class CmdResourcesSpend : ICommand{
        public readonly ResourceType ResourceType;
        public readonly int Amount;

        public CmdResourcesSpend(ResourceType resourceType, int amount){
            ResourceType = resourceType;
            Amount = amount;
        }
    }
}