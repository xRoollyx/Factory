using UnityEngine;

namespace myProject{
    public class Energy : BaseBuilding{
        private float timeOut = 2f;
        

        public Energy(BuildManager buildManager, BuildingsSo buildingsSo, int buildId) : base(buildManager, buildingsSo, buildId){
            
        }

        public override void Update(float deltaTime){
            timeOut -= deltaTime;
            if (timeOut < 0){
                Debug.Log($"Energy update {buildId}");
                timeOut = 2f;
            }
        }

        public override void Interact(){
            Debug.Log($"Energy  interact {buildId}");
        }
    }
}