using myProject.Scripts.BaCon.Scripts;

namespace myProject.Scripts.Game.Gameplay.Root.View{
    public static class GameplayViewModelsRegistrations{
        
        public static void Register(DiContainer container){
            container.RegisterFactory(c => new UIGameplayRootViewModel()).AsSingle();
            container.RegisterFactory(c=> new WorldGameplayRootViewModel()).AsSingle();
        }
    }
}