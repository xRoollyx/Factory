using System;
using System.Linq;
using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Commands;
using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.Settings;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.Providers;


namespace myProject.Scripts.Game.Gameplay.Root{
    public static class GameplayRegistrations{

        public static void Register(DiContainer container, GameplayEnterParams enterParams){
            var gameStateProvider = container.Resolve<IGameStateProvider>();
            var gameState = gameStateProvider.GameState;
            var gameSettingsProvider = container.Resolve<ISettingsProvider>();
            var gameSettings = gameSettingsProvider.GameSettings;
            
            var cmd = new CommandProcessor(gameStateProvider);
            cmd.RegisterHandler(new CmdPlaceBuildingHandler(gameState));
            cmd.RegisterHandler(new CmdCreateMapStateHandler(gameState, gameSettings));
            container.RegisterInstance<ICommandProcessor>(cmd);

            // загружаем карту по умолчанию из настроек
            var loadingMapId = enterParams.MapId;
            var loadingMap = gameState.Maps.FirstOrDefault(m => m.Id == loadingMapId);
            if (loadingMap == null){
                var command = new CmdCreateMapState(loadingMapId);
                var success = cmd.Process(command);
                if (!success){
                    throw new Exception($"Couldn't  create map {loadingMapId}");
                }
                loadingMap = gameState.Maps.First(m => m.Id == loadingMapId);
            }

            container.RegisterFactory(c => new BuildingsService(
                loadingMap.Buildings, 
                gameSettings.BuildingsSettings, 
                cmd)
            ).AsSingle();
        }
    }
}