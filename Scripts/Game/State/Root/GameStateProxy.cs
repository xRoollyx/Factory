using System.Linq;
using myProject.Scripts.Game.State.Maps;
using ObservableCollections;
using R3;


namespace myProject.Scripts.Game.State.Root{
    public class GameStateProxy{
        private readonly GameState _gameState;
        public ReactiveProperty<int> CurrentMapId = new();
        public ObservableList<Map>  Maps{ get; } =  new();

        public GameStateProxy(GameState gameState){
            _gameState = gameState;
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

            CurrentMapId.Subscribe(newValue => {
                gameState.CurrentMapId = newValue;
            });
        }

        public int CreateEntityId(){
            return _gameState.CreateEntityId();
        }
    }
}