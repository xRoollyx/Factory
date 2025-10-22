using System.Linq;
using myProject.Scripts.Game.State.GameResources;
using myProject.Scripts.Game.State.Maps;
using ObservableCollections;
using R3;


namespace myProject.Scripts.Game.State.Root{
    public class GameStateProxy{
        private readonly GameState _gameState;
        public ReactiveProperty<int> CurrentMapId = new();
        public ObservableList<Map>  Maps{ get; } =  new();
        public ObservableList<Resource> Resources{ get; } =  new();

        public GameStateProxy(GameState gameState){
            _gameState = gameState;
            
            InitializeMaps(gameState);
            InitializeResources(gameState);

            CurrentMapId.Subscribe(newValue => {
                gameState.CurrentMapId = newValue;
            });
        }

        public int CreateEntityId(){
            return _gameState.CreateEntityId();
        }

        private void InitializeMaps(GameState gameState){
            gameState.Maps.ForEach(mapOrigin => Maps.Add(new Map(mapOrigin)));

            Maps.ObserveAdd().Subscribe(e => {
                var addedMap = e.Value;
                gameState.Maps.Add(addedMap.Origin);
            });

            Maps.ObserveRemove().Subscribe(e => {
                var removedMap = e.Value;
                var removedMapState = gameState.Maps.FirstOrDefault(mapOrigin => mapOrigin.Id == removedMap.Id);
                gameState.Maps.Remove(removedMapState);
                
            });
        }

        private void InitializeResources(GameState gameState){
            gameState.Resources.ForEach(resourceData => Resources.Add(new Resource(resourceData)));

            Resources.ObserveAdd().Subscribe(e => {
                var addedResource = e.Value;
                gameState.Resources.Add(addedResource.Original);
            });

            Resources.ObserveRemove().Subscribe(e => {
                var removedResource = e.Value;
                var removedResourceData = gameState.Resources.FirstOrDefault(resourceOrigin =>
                    resourceOrigin.ResourceType == removedResource.ResourceType);
                gameState.Resources.Remove(removedResourceData);
            });
        }
    }
}