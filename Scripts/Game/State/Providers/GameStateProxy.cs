using ObservableCollections;
using R3;
using UnityEngine;

namespace myProject{
    public class GameStateProxy{
        
    }
    
    public class ChunkDataProxy{
        public int chunkSize { get; }
        public Vector2Int chunkCoordinate{ get; }
        public ObservableList<TileDataProxy> tileData{ get; } = new();

        public ChunkDataProxy(ChunkData chunkData){
            chunkSize = chunkData.chunkSize;
            chunkCoordinate = chunkData.chunkCoordinate;
            
            chunkData.tileData.ForEach(data => tileData.Add(new TileDataProxy(data)));

            tileData.ObserveAdd().Subscribe(@event => {
                var addedTile = @event.Value;
                chunkData.tileData.Add(new TileData{
                    worldCoordinate = addedTile.worldCoordinate,
                    localCoordinate = addedTile.localCoordinate,
                    tileType = addedTile.tileType.Value,
                });
            });
        }
    }
    
    public class TileDataProxy{
        public Vector2Int worldCoordinate { get;}
        public Vector2Int localCoordinate { get; }
        
        public ReactiveProperty<TileType> tileType{ get; }

        public TileDataProxy(TileData tileData){
            worldCoordinate = tileData.worldCoordinate;
            localCoordinate = tileData.localCoordinate;
            
            tileType = new ReactiveProperty<TileType>(tileData.tileType);
            
            tileType.Skip(1).Subscribe(value => tileData.tileType = value);
        }
    }
}