using myProject.Scripts.BaCon.Scripts;

namespace myProject.Scripts.Game.MainMenu.Root.View{
    public static class MainMenuViewModelsRegistrations{

        public static void Register(DiContainer container){
            container.RegisterFactory(c => new UIMainMenuRootViewModel()).AsSingle();
        }
    }
}