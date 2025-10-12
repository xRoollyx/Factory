using myProject.Scripts.Game.Gameplay.Services;

namespace myProject.Scripts.Game.Gameplay.Root.View{
    public class UIGameplayRootViewModel{
        private readonly SomeGameplayService  _gameplayService;

        public UIGameplayRootViewModel(SomeGameplayService gameplayService){
            _gameplayService = gameplayService;
        }
    }
}