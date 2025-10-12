using myProject.Scripts.Common;
using myProject.Scripts.Game.GameRoot;

namespace myProject.Scripts.Game.Gameplay.Root{
    public class GameplayEnterParams : SceneEnterParams{
        
        public string SaveFileName { get;}
        public int LevelNumber { get;}
        public GameplayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY){
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}