using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Services;

namespace myProject.Scripts.Game.Gameplay.Root.View{
    public static class GameplayViewModelsRegistrations{
        
        public static void Register(DiContainer container){
            container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
            container.RegisterFactory(c=> new WorldGameplayRootViewModel(
                c.Resolve<BuildingsService>()
                )).AsSingle();
        }
    }
}