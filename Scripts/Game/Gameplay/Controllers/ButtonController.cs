using System;
using UnityEngine;

namespace myProject{
    public class ButtonController : MonoBehaviour{
        private BuildManager _buildManager;
        private InputController _inputController;
        
        
        
        public void Initialize(BuildManager buildManager, InputController inputController){
            _buildManager = buildManager;
            _inputController = inputController;
        }

        public void ConstructBuilding(int buildingIndex){
            _buildManager.CreateNewBuilding(buildingIndex);
        }
    }
}