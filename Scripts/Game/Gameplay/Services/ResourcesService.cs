using System;
using System.Collections.Generic;
using System.Linq;
using myProject.Scripts.Game.Gameplay.Commands;
using myProject.Scripts.Game.Gameplay.View.GameResources;
using myProject.Scripts.Game.State.cmd;
using myProject.Scripts.Game.State.GameResources;
using ObservableCollections;
using R3;

namespace myProject.Scripts.Game.Gameplay.Services{
    public class ResourcesService{
        public readonly ObservableList<ResourceViewModel> Resources = new ();
        
        private readonly Dictionary<ResourceType, ResourceViewModel> _resourcesMap = new ();
        private readonly ICommandProcessor _cmd;

        public ResourcesService(ObservableList<Resource>  resources, ICommandProcessor cmd){
            _cmd = cmd;
            
            resources.ForEach(CreateResourceViewModel);
            resources.ObserveAdd().Subscribe(e => {
                CreateResourceViewModel(e.Value);
            });
            
            resources.ObserveRemove().Subscribe(e => {
                RemoveResourceViewModel(e.Value);
            });
        }

        public bool AddResource(ResourceType resourceType, int amount){
            var command = new CmdResourcesAdd(resourceType, amount);

            return _cmd.Process(command);
        }

        public bool TrySpendResource(ResourceType resourceType, int amount){
            var command = new CmdResourcesSpend(resourceType, amount);
            
            return _cmd.Process(command);
        }

        public bool IsEnoughResource(ResourceType resourceType, int amount){
            if (_resourcesMap.TryGetValue(resourceType, out var resource)){
                return resource.Amount.CurrentValue >= amount;
                
            }
            return false;
        }

        public Observable<int> ObserveResource(ResourceType resourceType){
            if (_resourcesMap.TryGetValue(resourceType, out var resource)){
                return resource.Amount;
            }
            throw new Exception($"Resource type {resourceType} is not supported");
        }

        private void CreateResourceViewModel(Resource resource){
            var resourceViewModel = new ResourceViewModel(resource);
            _resourcesMap[resource.ResourceType] = resourceViewModel;
            
            Resources.Add(resourceViewModel);
        }
        
        private void RemoveResourceViewModel(Resource resource){
            if (_resourcesMap.TryGetValue(resource.ResourceType, out var resourceViewModel)){
                Resources.Remove(resourceViewModel);
                _resourcesMap.Remove(resource.ResourceType);
            }
        }
    }
}