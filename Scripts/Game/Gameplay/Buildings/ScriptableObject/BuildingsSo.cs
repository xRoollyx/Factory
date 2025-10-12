using UnityEngine;
using UnityEngine.Serialization;

namespace myProject{
    [CreateAssetMenu(fileName = "BuildingsSO",menuName = "MyProject/BuildingsSO/Buildings")]
    public class BuildingsSo : ScriptableObject{
        public GameObject buildingPrefab;
        public BuildingsType buildingsType;
        public Vector2Int buildSize;
    }
}
