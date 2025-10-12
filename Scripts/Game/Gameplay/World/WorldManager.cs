using UnityEngine;

namespace myProject{
    public class WorldManager : MonoBehaviour{
        private WorldData _worldData;
        private WorldVisualManager _worldVisualManager;
        private WorldGeneration _worldGeneration;


        private World _world;
        public Vector2Int worldSize => _worldData.worldSize;

        
        public void Initialize(WorldData worldData, WorldVisualManager worldVisualManager, WorldGeneration worldGeneration){
            _worldData = worldData;
            _worldVisualManager = worldVisualManager;
            _worldGeneration = worldGeneration;
            
            CreateWorld(_worldData);
            _worldVisualManager.Initialize(this, _worldData);
            _worldGeneration.Initialize(this);
        }

        private void CreateWorld(WorldData worldData){
            _world = new World(worldData);
        }
        
        public bool CheckPossibilityBuildings(BaseBuilding building){
            Vector2Int buildSize = building.buildingsSo.buildSize;
            for (int y = building.position.Value.y; y < building.position.Value.y + buildSize.y; y++){
                for (int x = building.position.Value.x; x < building.position.Value.x + buildSize.x; x++){
                    var tile = _world.GetTileToWorldCoordinate(new Vector2Int(x, y));
                    if (tile is{ type: TileType.Grass, isEmpty: true }){
                    }
                    else{
                        return false;
                    }
                }
            }
            return true;
        }
        
        public void CreateBuildingsToTile(BaseBuilding building){
            
            Vector2Int buildSize = building.buildingsSo.buildSize;
            for (int y = building.position.Value.y; y < building.position.Value.y + buildSize.y; y++){
                for (int x = building.position.Value.x; x < building.position.Value.x + buildSize.x; x++){
                    var tile = _world.GetTileToWorldCoordinate(new Vector2Int(x, y));
                    if (tile is{ type: TileType.Grass, isEmpty: true }){
                        building.SetTile(tile);
                        tile.BuildBuilding(building);
                    }
                }
            }
        }

        public Tile GetTileToWorldCoordinate(Vector2Int coordinate){
            return _world.GetTileToWorldCoordinate(coordinate);
        }

        public Chunk GetChunkToChunkCoordinate(Vector2Int chunkCoordinate){
            return _world.GetChunkToChunkCoordinate(chunkCoordinate);
        }
    }
}