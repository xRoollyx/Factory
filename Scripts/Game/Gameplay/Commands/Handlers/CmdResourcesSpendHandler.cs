using System.Linq;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.Root;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.Commands{
    public class CmdResourcesSpendHandler: ICommandHandler<CmdResourcesSpend>{
        private readonly GameStateProxy  _gameState;

        public CmdResourcesSpendHandler(GameStateProxy gameState){
            _gameState = gameState;
        }

        public bool Handle(CmdResourcesSpend command){
            var requiredResourceType = command.ResourceType;
            var requiredResource = _gameState.Resources.FirstOrDefault(r => r.ResourceType == requiredResourceType);
            if (requiredResource == null){
                Debug.LogError("Trying to spend not existing resource");
                return false;
            }

            if (requiredResource.Amount.Value < command.Amount){
                Debug.LogWarning($"Trying to spend more resources{requiredResource.ResourceType} then {requiredResource.Amount}");
                return false;
            }
            requiredResource.Amount.Value -= command.Amount;
            return true;
        }
    }
}