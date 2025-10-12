using System.Collections.Generic;
using UnityEngine;

namespace myProject{
    public class CreateGameStateFromSettings{
        
        private Vector2Int _worldSize;
        private readonly int _chunkSize;

        public CreateGameStateFromSettings(){
            _worldSize = Const.worldSize;
            _chunkSize = Const.chunkSize;
        }
        
        public GameState GetNewGameState(){
            var gameState = new GameState();
            gameState.worldData = CreateDefaultWorldData();
            gameState.cameraData = new CameraData{
                position = new Vector3(15, 15, -40),
                zoom = 1f
            };
            gameState.buildings = new List<BuildingData>();
            return gameState;
        }
        private WorldData CreateDefaultWorldData(){
            WorldData worldData = new WorldData{
                worldSize = _worldSize,
                chunkSize = _chunkSize,
                chunksData = new ChunkData[_worldSize.x * _worldSize.y]
            };
            for (int chunkY = 0; chunkY < _worldSize.y; chunkY++){
                for (int chunkX = 0; chunkX < _worldSize.x; chunkX++){
                    
                    Vector2Int chunkCoordinate = new Vector2Int(chunkX, chunkY);
                    int indexChunk = chunkX + chunkY * _worldSize.x;

                    worldData.chunksData[indexChunk] = new ChunkData{
                        chunkSize = _chunkSize,
                        chunkCoordinate = chunkCoordinate,
                        tileData = new List<TileData>()
                    };
                    for (int y = 0; y < _chunkSize; y++){
                        for (int x = 0; x < _chunkSize; x++){
                            int index = x + y * _chunkSize;
                            Vector2Int localCoordinate = new Vector2Int(x, y);
                            if (worldData.chunksData[indexChunk].tileData[index] == null){
                                worldData.chunksData[indexChunk].tileData[index] = new TileData{
                                    localCoordinate = localCoordinate,
                                    worldCoordinate = Utilit.GetWorldCoordinate(localCoordinate, worldData.chunksData[indexChunk].chunkCoordinate),
                                    tileType = TileType.Sand
                                };
                            }
                        }
                    }
                }
            }

            return worldData;
        }
    }
}