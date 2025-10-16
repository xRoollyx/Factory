using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Commands;
using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.Providers;


namespace myProject.Scripts.Game.Gameplay.Root{
    public static class GameplayRegistrations{

        public static void Register(DiContainer container, GameplayEnterParams enterParams){
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;
            
            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameState));
            container.RegisterInstance<ICommandProcessor>(cmd);
            
            container.RegisterFactory(c => new BuildingsService(gameState.Buildings, cmd)).AsSingle();
        }
    }
}