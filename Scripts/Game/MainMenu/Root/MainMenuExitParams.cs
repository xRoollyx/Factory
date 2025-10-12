using myProject.Scripts.Game.GameRoot;

namespace myProject{
    public class MainMenuExitParams{
        public SceneEnterParams TargetSceneEnterParams{ get; }

        public MainMenuExitParams(SceneEnterParams targetSceneEnterParams){
            TargetSceneEnterParams = targetSceneEnterParams;
        }
    }
}