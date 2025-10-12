using System;
using System.Collections.Generic;
using myProject.Scripts.Game.State.Buildings;

namespace myProject.Scripts.Game.State.Root{
    [Serializable]
    public class GameState{
        public List<BuildingEntity> Buildings;
    }

}