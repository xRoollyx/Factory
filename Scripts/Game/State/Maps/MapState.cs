using System;
using System.Collections.Generic;
using myProject.Scripts.Game.State.Buildings;

namespace myProject.Scripts.Game.State.Maps{
    [Serializable]
    public class MapState{
        public int Id;
        public List<BuildingEntity> Buildings;
    }
}