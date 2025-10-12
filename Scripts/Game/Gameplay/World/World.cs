using System.Collections.Generic;
using UnityEngine;

namespace myProject{
    public  class World{
        private readonly Dictionary<Vector2Int, Chunk> _chunks; 

        public World(WorldData worldData){
            _chunks = new Dictionary<Vector2Int, Chunk>();
            CreateChunks(worldData);
        }

        private void CreateChunks(WorldData worldData){
            var worldSize = worldData.worldSize;
            for (int y = 0; y < worldSize.y; y++){
                for (int x = 0; x < worldSize.x; x++){
                    var chunkCoordinate = new Vector2Int(x, y);
                    int index = x + y * worldSize.x;
                    var chunkData = worldData.chunksData[index];
                    CreateChunk(chunkCoordinate, chunkData);
                }
            }
        }

        private void CreateChunk(Vector2Int chunkCoordinate, ChunkData chunkData){
            Chunk chunk = new Chunk(this, chunkData);
            _chunks.Add(chunkCoordinate, chunk);
        }


        public Tile GetTileToWorldCoordinate(Vector2Int worldCoordinate){
            Chunk chunk = _chunks.GetValueOrDefault(Utilit.GetChunkCoordinateToWorldCoordinate(worldCoordinate));
            if (chunk == null){
                return null;
            }
            Tile tile = chunk.GetTileToLocalCoordinate(Utilit.GetLocalTileCoordinateToWorldCoordinate(worldCoordinate));
            return tile;
        }
    
        public Chunk GetChunkToChunkCoordinate(Vector2Int chunkCoordinate){
            return _chunks.GetValueOrDefault(chunkCoordinate);
        }
    }
}