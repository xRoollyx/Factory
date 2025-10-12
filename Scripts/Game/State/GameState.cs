using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace myProject{
    [Serializable]
    public class GameState{
        public WorldData worldData;
        public CameraData cameraData;
        public List<BuildingData> buildings;
        public int globalEntityId;
        public MoneyData moneyData;

        public int CreateEntityId(){
            return globalEntityId++;
        }
    }

    [Serializable]
    public class WorldData{
        public Vector2Int worldSize;
        public int chunkSize;
        public ChunkData[] chunksData;
    }
    [Serializable]
    public class ChunkData{
        public int chunkSize;
        public Vector2Int chunkCoordinate;
        public List<TileData> tileData;
    }

    [Serializable]
    public class TileData{
        public Vector2Int worldCoordinate;
        public Vector2Int localCoordinate;
        public TileType tileType;
    }

    [Serializable]
    public class CameraData{
        public Vector3 position;
        public float zoom;
    }

    [Serializable]
    public class BuildingData{
        public int buildId;
        public BuildingsType buildingType;
        public Vector2Int position;
    }

    [Serializable]
    public class MoneyData{
        public int gold;
    }
}