using myProject.Scripts.Game.State.cmd;

namespace myProject.Scripts.Game.Gameplay.Commands{
    public class CmdCreateMapState: ICommand{
        public readonly int MapId;

        public CmdCreateMapState(int mapId){
            MapId = mapId;
        }
    }
}