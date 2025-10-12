using myProject.Scripts.Game.MainMenu.Root;

namespace myProject.Scripts.Game.Gameplay.Root{
    public class GameplayExitParams{
        public MainMenuEnterParams mainMenuEnterParams{ get; }

        public GameplayExitParams(MainMenuEnterParams mainMenuEnterParams){
            this.mainMenuEnterParams = mainMenuEnterParams;
        }
    }
}