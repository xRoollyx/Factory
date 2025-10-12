using myProject.Scripts.BaCon.Scripts;
using myProject.Scripts.Game.Gameplay.Root;
using myProject.Scripts.Game.GameRoot.Services;
using myProject.Scripts.Game.MainMenu.Services;

namespace myProject.Scripts.Game.MainMenu.Root{
    public static class MainMenuRegistrations{
        public static void Register(DiContainer container, MainMenuEnterParams enterParams){
            container.RegisterFactory(c => new SomeMainMenuService(c.Resolve<SomeCommonService>())).AsSingle();
        }
    }
}