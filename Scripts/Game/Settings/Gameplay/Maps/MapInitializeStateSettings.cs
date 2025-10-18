using System;
using System.Collections.Generic;
using myProject.Scripts.Game.Settings.Gameplay.Buildings;

namespace myProject.Scripts.Game.Settings.Gameplay.Maps{
    [Serializable]
    public class MapInitializeStateSettings{
        public List<BuildingInitialStateSettings>  Buildings;
    }
}