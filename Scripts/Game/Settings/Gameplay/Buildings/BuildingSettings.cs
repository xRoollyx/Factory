using System.Collections.Generic;
using UnityEngine;

namespace myProject.Scripts.Game.Settings.Gameplay.Buildings{
    [CreateAssetMenu(fileName = "BuildingSettings", menuName = "Game Settings/Buildings/New Building Settings")]
    public class BuildingSettings: ScriptableObject{
        public string TypeId;
        public string TitleLid;
        public string DescriptionLid;
        public List<BuildingLevelSettings> LevelSettings;
    }
}