using System.Collections.Generic;
using UnityEngine;

namespace myProject{
    public class WorldVisualManager : MonoBehaviour{

        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private TileSpritesSo tileSpritesSo;
        
        private readonly Dictionary<Vector2Int, TileVisual> _tileVisuals = new();
        private WorldManager _worldManager;
        
        private Vector2Int _worldSize;

        public void Initialize(WorldManager worldManager, WorldData worldData){
            _worldManager = worldManager;
            
            
            _worldSize = worldData.worldSize;
            CreateVisualTile(worldData);
        }

        private void CreateVisualTile(WorldData worldData){
            for (int y = 0; y < _worldSize.y; y++){
                for (int x = 0; x < _worldSize.x; x++){
                    int index = x + y * _worldSize.x;
                    var chunkData = worldData.chunksData[index];
                    
                    CreateTileGameObject(chunkData);
                }
            }
        }
    
    
        public void CreateTileGameObject(ChunkData chunkData){
            var chunkSize = chunkData.chunkSize;
            for (int y = 0; y < chunkSize; y++){
                for (int x = 0; x < chunkSize; x++){
                    int index = x + y * chunkSize;
                    Vector2Int worldCoordinate = chunkData.tileData[index].worldCoordinate;
                    var tileGo = Instantiate(tilePrefab, new Vector3(worldCoordinate.x, worldCoordinate.y, 0), Quaternion.identity, this.transform);
                    
                    TileVisual tileVisual = tileGo.GetComponent<TileVisual>();
                    tileVisual.Initialize(tileSpritesSo);
                    tileVisual.SetSprite((int)chunkData.tileData[index].tileType);
                    var tile = _worldManager.GetTileToWorldCoordinate(worldCoordinate);
                    tile.onTileTypeChanged.AddListener(tileVisual.SetSprite);
                    _tileVisuals.Add(worldCoordinate, tileVisual);
                }
            }
        }

        public TileVisual GetTileVisual(Vector2Int worldCoordinate){
            return _tileVisuals[worldCoordinate];
        }
    }
}
