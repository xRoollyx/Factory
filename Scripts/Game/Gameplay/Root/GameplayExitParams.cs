namespace myProject.Scripts.Game.Gameplay.Root{
    public class GameplayExitParams{
        public MainMenuExitParams MainMenuExitParams{ get; }

        public GameplayExitParams(MainMenuExitParams mainMenuExitParams){
            MainMenuExitParams = mainMenuExitParams;
        }
    }
}