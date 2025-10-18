using myProject.Scripts.Common;
using myProject.Scripts.Game.GameRoot;

namespace myProject.Scripts.Game.Gameplay.Root{
    public class GameplayEnterParams : SceneEnterParams{
        
        
        public int MapId { get;}
        public GameplayEnterParams(int mapId) : base(Scenes.GAMEPLAY){
            
            MapId = mapId;
        }
    }
}