using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.GameRoot.Services;
using myProject.Scripts.Game.State.Providers;

namespace myProject.Scripts.Game.Gameplay.Root{
    public static class GameplayRegistrations{

        public static void Register(DiContainer container, GameplayEnterParams enterParams){
            container.RegisterFactory(c => new SomeGameplayService(
                c.Resolve<IGameStateProvider>().GameState,
                c.Resolve<SomeCommonService>()
                )).AsSingle();
        }
    }
}