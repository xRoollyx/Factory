using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.View.Buildings{
    public class BuildingBinder: MonoBehaviour{

        public void Bind(BuildingViewModel buildingViewModel){
            transform.position = buildingViewModel.Position.CurrentValue;
        }
    }
}