using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace myProject{
    public class Chunk{
        private readonly int _chunkSize;
        private readonly World _world;
        private Vector2Int _chunkCoordinate;
        private readonly Dictionary<Vector2Int, Tile> _tiles = new();
    
        public Chunk(World world, ChunkData chunkData){
            _world = world;
            _chunkSize = chunkData.chunkSize;
            _chunkCoordinate = chunkData.chunkCoordinate;
            CreateChunk(chunkData);
        }

        private void CreateChunk(ChunkData chunkData){
            for (int y = 0; y < _chunkSize; y++){
                for (int x = 0; x < _chunkSize; x++){
                    int index = x + y * _chunkSize;
                    Vector2Int localCoordinate = new Vector2Int(x, y);
                    
                    CreateTile(localCoordinate,chunkData.tileData[index]);
                }
            }
        }


        private void CreateTile(Vector2Int localCoordinate, TileData data){
            var tile = new Tile(data);
            
            _tiles[localCoordinate] = tile;

            SetNeighbors(tile, localCoordinate);
        }

        private void SetNeighbors(Tile tile, Vector2Int localCoordinate){
            int chunkX = _chunkCoordinate.x;
            int chunkY = _chunkCoordinate.y;
            Chunk chunkN = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX, chunkY + 1));
            Chunk chunkNe = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX + 1, chunkY + 1));
            Chunk chunkE = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX + 1, chunkY));
            Chunk chunkSe = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX + 1, chunkY - 1));
            Chunk chunkS = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX, chunkY - 1));
            Chunk chunkSw = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX - 1, chunkY - 1));
            Chunk chunkW = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX - 1, chunkY));
            Chunk chunkNw = _world.GetChunkToChunkCoordinate(new Vector2Int(chunkX - 1, chunkY + 1));
    
            int borderChunkMax = _chunkSize - 1;
            int borderChunkMin = 0;
    
            if (localCoordinate.x == borderChunkMin && localCoordinate.y == borderChunkMin){
                if (chunkW != null){
                    tile.SetNeighbor(Direction.Nw,
                        chunkW.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, localCoordinate.y + 1)));
                    tile.SetNeighbor(Direction.W, chunkW.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, localCoordinate.y)));
                }
    
                if (chunkSw != null){
                    tile.SetNeighbor(Direction.Sw, chunkSw.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, borderChunkMax)));
                }
    
                if (chunkS != null){
                    tile.SetNeighbor(Direction.S, chunkS.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, borderChunkMax)));
                    tile.SetNeighbor(Direction.Se,
                        chunkS.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x + 1, borderChunkMax)));
                }
            }
    
            if (localCoordinate.x > borderChunkMin && localCoordinate.x < borderChunkMax &&
                localCoordinate.y == borderChunkMin){
                tile.SetNeighbor(Direction.W, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y)]);
                if (chunkS != null){
                    tile.SetNeighbor(Direction.Sw,
                        chunkS.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x - 1, borderChunkMax)));
                    tile.SetNeighbor(Direction.S, chunkS.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x, borderChunkMax)));
                    tile.SetNeighbor(Direction.Se,
                        chunkS.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x + 1, borderChunkMax)));
                }
            }
    
            if (localCoordinate.x == borderChunkMax && localCoordinate.y == borderChunkMin){
                tile.SetNeighbor(Direction.W, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y)]);
                if (chunkS != null){
                    tile.SetNeighbor(Direction.Sw,
                        chunkS.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x - 1, borderChunkMax)));
                    tile.SetNeighbor(Direction.S, chunkS.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, borderChunkMax)));
                }
    
                if (chunkSe != null){
                    tile.SetNeighbor(Direction.Se, chunkSe.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, borderChunkMax)));
                }
    
                if (chunkE != null){
                    tile.SetNeighbor(Direction.E, chunkE.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, borderChunkMin)));
                    tile.SetNeighbor(Direction.Ne,
                        chunkE.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, localCoordinate.y + 1)));
                }
            }
    
            if (localCoordinate.x == borderChunkMin && localCoordinate.y > borderChunkMin &&
                localCoordinate.y < borderChunkMax){
                if (chunkW != null){
                    tile.SetNeighbor(Direction.Nw,
                        chunkW.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, localCoordinate.y + 1)));
                    tile.SetNeighbor(Direction.W, chunkW.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, localCoordinate.y)));
                    tile.SetNeighbor(Direction.Sw,
                        chunkW.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, localCoordinate.y - 1)));
                }
    
                tile.SetNeighbor(Direction.S, _tiles[new Vector2Int(localCoordinate.x, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.Se, _tiles[new Vector2Int(localCoordinate.x + 1, localCoordinate.y - 1)]);
            }
    
            if (localCoordinate.x > borderChunkMin && localCoordinate.x < borderChunkMax &&
                localCoordinate.y > borderChunkMin && localCoordinate.y < borderChunkMax){
                tile.SetNeighbor(Direction.W, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y)]);
                tile.SetNeighbor(Direction.Sw, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.S, _tiles[new Vector2Int(localCoordinate.x, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.Se, _tiles[new Vector2Int(localCoordinate.x + 1, localCoordinate.y - 1)]);
            }
    
            if (localCoordinate.x == borderChunkMin && localCoordinate.y == borderChunkMax){
                if (chunkN != null){
                    tile.SetNeighbor(Direction.Ne,
                        chunkN.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x + 1, borderChunkMin)));
                    tile.SetNeighbor(Direction.N, chunkN.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, borderChunkMin)));
                }
    
                if (chunkNw != null){
                    tile.SetNeighbor(Direction.Nw, chunkNw.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, borderChunkMin)));
                }
    
                if (chunkW != null){
                    tile.SetNeighbor(Direction.W, chunkW.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, borderChunkMax)));
                    tile.SetNeighbor(Direction.Sw,
                        chunkW.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, localCoordinate.y - 1)));
                }
    
                tile.SetNeighbor(Direction.S, _tiles[new Vector2Int(localCoordinate.x, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.Se, _tiles[new Vector2Int(localCoordinate.x + 1, localCoordinate.y - 1)]);
            }
    
            if (localCoordinate.x == borderChunkMax && localCoordinate.y > borderChunkMin &&
                localCoordinate.y < borderChunkMax){
                tile.SetNeighbor(Direction.W, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y)]);
                tile.SetNeighbor(Direction.Sw, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.S, _tiles[new Vector2Int(localCoordinate.x, localCoordinate.y - 1)]);
                if (chunkE != null){
                    tile.SetNeighbor(Direction.Se,
                        chunkE.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, localCoordinate.y - 1)));
                    tile.SetNeighbor(Direction.E, chunkE.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, localCoordinate.y)));
                    tile.SetNeighbor(Direction.Ne,
                        chunkE.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, localCoordinate.y + 1)));
                }
            }
    
            if (localCoordinate.x > borderChunkMin && localCoordinate.x < borderChunkMax &&
                localCoordinate.y == borderChunkMax){
                tile.SetNeighbor(Direction.W, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y)]);
                tile.SetNeighbor(Direction.Sw, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.S, _tiles[new Vector2Int(localCoordinate.x, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.Se, _tiles[new Vector2Int(localCoordinate.x + 1, localCoordinate.y - 1)]);
                if (chunkN != null){
                    tile.SetNeighbor(Direction.Ne,
                        chunkN.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x + 1, borderChunkMin)));
                    tile.SetNeighbor(Direction.N, chunkN.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x, borderChunkMin)));
                    tile.SetNeighbor(Direction.Nw,
                        chunkN.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x - 1, borderChunkMin)));
                }
            }
    
            if (localCoordinate.x == borderChunkMax && localCoordinate.y == borderChunkMax){
                tile.SetNeighbor(Direction.W, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y)]);
                tile.SetNeighbor(Direction.Sw, _tiles[new Vector2Int(localCoordinate.x - 1, localCoordinate.y - 1)]);
                tile.SetNeighbor(Direction.S, _tiles[new Vector2Int(localCoordinate.x, localCoordinate.y - 1)]);
                if (chunkE != null){
                    tile.SetNeighbor(Direction.Se,
                        chunkE.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, localCoordinate.y - 1)));
                    tile.SetNeighbor(Direction.E, chunkE.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, borderChunkMax)));
                }
    
                if (chunkNe != null){
                    tile.SetNeighbor(Direction.Ne, chunkNe.GetTileToLocalCoordinate(new Vector2Int(borderChunkMin, borderChunkMin)));
                }
    
                if (chunkN != null){
                    tile.SetNeighbor(Direction.N, chunkN.GetTileToLocalCoordinate(new Vector2Int(borderChunkMax, borderChunkMin)));
                    tile.SetNeighbor(Direction.Nw,
                        chunkN.GetTileToLocalCoordinate(new Vector2Int(localCoordinate.x - 1, borderChunkMin)));
                }
            }
        }
    
        public Tile GetTileToLocalCoordinate(Vector2Int localCoordinate){
            Tile tile = _tiles[localCoordinate];
            return tile;
        }

        public Tile[] GetChunkTiles(){
            Tile[] tiles = _tiles.Values.ToArray();
            return tiles;
        }
    }
}