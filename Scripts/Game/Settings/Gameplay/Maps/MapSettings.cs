using UnityEngine;
using UnityEngine.Serialization;

namespace myProject.Scripts.Game.Settings.Gameplay.Maps{
    [CreateAssetMenu(fileName = "MapSettings", menuName = "Game Settings/Maps/New Map Settings")]
    public class MapSettings: ScriptableObject{
        public int MapId;
        public MapInitializeStateSettings InitializeStateSettings;
    }
}