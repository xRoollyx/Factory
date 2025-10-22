using System;
using System.Collections.Generic;
using myProject.Scripts.Game.State.GameResources;
using myProject.Scripts.Game.State.Maps;

namespace myProject.Scripts.Game.State.Root{
    [Serializable]
    public class GameState{
        public int GlobalEntityId;
        public int CurrentMapId;
        public List<MapState> Maps;
        public List<ResourceData>  Resources;

        public int CreateEntityId(){
            return GlobalEntityId++;
        }
    }

}