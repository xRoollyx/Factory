using System.Linq;
using R3;
using UnityEngine;

namespace myProject{
    public class BuildingFactory : MonoBehaviour{
        [SerializeField] BuildingsSo[] buildingsSo;

        public BaseBuilding CreateNewBuilding(BuildingsType buildingsType, BuildManager buildManager){
            var buildingSo = buildingsSo.First(buildSo => buildSo.buildingsType == buildingsType);

            switch (buildingsType){
                case BuildingsType.Refinery:
                    var buildGo = Instantiate(buildingSo.buildingPrefab, transform);
                    BaseBuilding building = new Refinery(buildManager, buildingSo, buildManager.GetBuildId());
                    var buildVisual = buildGo.GetComponent<BuildingsVisual>();
                    buildVisual.Init(building.position, building.color);
                    building.destroyBuilding.Subscribe(f => buildVisual.DestroyGo());
                    
                    return building;
            }

            return null;
        }
        public BaseBuilding CreateBuilding(BuildingsType buildingsType, BuildManager buildManager, int buildId){
            var buildingSo = buildingsSo.First(buildSo => buildSo.buildingsType == buildingsType);

            switch (buildingsType){
                case BuildingsType.Refinery:
                    var buildGo = Instantiate(buildingSo.buildingPrefab, transform);
                    BaseBuilding building = new Refinery(buildManager, buildingSo, buildId);
                    var buildVisual = buildGo.GetComponent<BuildingsVisual>();
                    buildVisual.Init(building.position, building.color);
                    building.destroyBuilding.Subscribe(f => buildVisual.DestroyGo());
                    
                    return building;
            }

            return null;
        }
    }
}