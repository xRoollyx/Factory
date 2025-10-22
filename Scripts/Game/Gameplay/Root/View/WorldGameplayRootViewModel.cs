using System;
using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.Gameplay.View.Buildings;
using myProject.Scripts.Game.State.GameResources;
using ObservableCollections;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

namespace myProject.Scripts.Game.Gameplay.Root.View{
    public class WorldGameplayRootViewModel{
        private readonly ResourcesService _resourcesService;

        public readonly IObservableCollection<BuildingViewModel> AllBuildings;

        public WorldGameplayRootViewModel(BuildingsService buildingsService, ResourcesService resourcesService){
            _resourcesService = resourcesService;
            AllBuildings = buildingsService.AllBuildings;

            resourcesService.ObserveResource(ResourceType.SoftCurrency).Subscribe(newValue => {
                Debug.Log($"SoftCurrency: {newValue}");
            });

            resourcesService.ObserveResource(ResourceType.HardCurrency).Subscribe(newValue => {
                Debug.Log($"HardCurrency: {newValue}");
            });
        }

        public void HandleTestInput(){
            var rResourceType = (ResourceType)Random.Range(0, Enum.GetValues(typeof(ResourceType)).Length);
            var rValue = Random.Range(0, 100);
            var rOperation = Random.Range(0, 2);

            if (rOperation == 0){
                _resourcesService.AddResource(rResourceType, rValue);
                return;
            }
            
            _resourcesService.TrySpendResource(rResourceType, rValue);
        }
        
    }
}