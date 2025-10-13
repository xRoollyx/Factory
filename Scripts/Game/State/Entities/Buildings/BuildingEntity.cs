using System;
using myProject.Scripts.Game.State.Entities;
using UnityEngine;

namespace myProject.Scripts.Game.State.Buildings{
    [Serializable]
    public class BuildingEntity: Entity{
    
        public string TypeId;
        public Vector3Int Position;
        public int Level;
    }
}