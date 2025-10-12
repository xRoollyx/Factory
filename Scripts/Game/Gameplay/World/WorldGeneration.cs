using System.Globalization;
using UnityEngine;
using Random = System.Random;

namespace myProject{
    [ExecuteInEditMode]
    public class WorldGeneration : MonoBehaviour{
        private WorldManager _worldManager;
        [Range(0,100)] public int sand;
        [Range(0,100)] public int grass;
    
        private float _sandPercent;
        private float _grassPercent;
   
        private Random _pseudoRandom;
    
        public string seed;
        public bool useRandomSeed;


        public void Initialize(WorldManager worldManager){
            _worldManager = worldManager;
            RecalculatePercent();
        }

        public void RecalculatePercent(){
            if (useRandomSeed){
                seed = Time.time.ToString(CultureInfo.DefaultThreadCurrentCulture);
            }
            _pseudoRandom = new Random(seed.GetHashCode());
        
            var coofCient = grass  + sand;
            _sandPercent = ((float)sand / coofCient) * 100;
            _grassPercent = ((float)grass / coofCient) * 100 + _sandPercent;
        }

        private int RandomType(){
            int type;
            var prnd = _pseudoRandom.Next(0, 100);
            if (prnd < _sandPercent){
                type = 0;
                return type;
            }else if (prnd < _grassPercent){
                type = 1;
                return type;
            }

            return 0;
        }

        private void RandomTileType(){
            for (int y = 0; y < _worldManager.worldSize.y; y++){
                for (int x = 0; x < _worldManager.worldSize.x; x++){
                    Chunk chunk = _worldManager.GetChunkToChunkCoordinate(new Vector2Int(x, y));
                    foreach (var tile in chunk.GetChunkTiles()){
                        tile.type = (TileType)RandomType();
                    }
                }
            }
        }

        private void SmoothTileType(){
            for (int y = 0; y < _worldManager.worldSize.y; y++){
                for (int x = 0; x < _worldManager.worldSize.x; x++){
                    Chunk chunk = _worldManager.GetChunkToChunkCoordinate(new Vector2Int(x, y));
                    foreach (var tile in chunk.GetChunkTiles()){
                        int sand = 0;
                        int grass = 0;
                        Tile[] tiles = tile.GetNeighbors();
                        foreach (var neigbor in tiles){
                            if (neigbor != null){
                                if (neigbor.type == TileType.Sand){
                                    sand++;
                                }

                                if (neigbor.type == TileType.Grass){
                                    grass++;
                                }
                            }
                            else{
                                tile.type = TileType.Sand;
                            }
                        }

                        if (sand > 4){
                            tile.type = TileType.Sand;
                        }

                        if (grass > 4){
                            tile.type = TileType.Grass;
                        }
                    }
                }
            }
        }

        public void ResetTileType(){
            RandomTileType();
        }
    
        public void Smooth(){
            SmoothTileType();
        }
    }
}
