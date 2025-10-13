using System;
using System.Linq;
using myProject.Scripts.Game.GameRoot.Services;
using myProject.Scripts.Game.State.Buildings;
using myProject.Scripts.Game.State.Root;
using ObservableCollections;
using R3;
using UnityEngine;

namespace myProject.Scripts.Game.Gameplay.Services{
    public class SomeGameplayService: IDisposable{
        private readonly GameStateProxy _gameState;
        private readonly SomeCommonService  _someCommonService;

        public SomeGameplayService(GameStateProxy gameState,SomeCommonService someCommonService){
            _gameState = gameState;
            _someCommonService = someCommonService;
            Debug.Log(GetType().Name + "has been created");
            
            // gameState.Buildings.ForEach(building => Debug.Log("building " + building.TypeId));
            // gameState.Buildings.ObserveAdd().Subscribe(e =>Debug.Log("building added " +e.Value.TypeId));
            // gameState.Buildings.ObserveRemove().Subscribe(e =>Debug.Log("building removed " +e.Value.TypeId));
            //
            // AddBuilding("VASYAN");
            // AddBuilding("STAS");
            // RemoveBuilding("VASYAN");
        }

        private void AddBuilding(string typeId){
            var building = new BuildingEntity{
                TypeId = typeId
            };
            var buildingProxy = new BuildingEntityProxy(building);
            _gameState.Buildings.Add(buildingProxy);
        }

        private void RemoveBuilding(string typeId){
            var buildingEntity = _gameState.Buildings.FirstOrDefault(b => b.TypeId == typeId);
            if (buildingEntity != null){
                _gameState.Buildings.Remove(buildingEntity);
            }
        }
        
        public void Dispose(){
            Debug.Log("SomeGameplayService disposed");
        }
    }
}