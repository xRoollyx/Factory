using UnityEngine;

namespace myProject{
    public static class Utilit {
    
        public static Vector2Int GetChunkCoordinateToWorldCoordinate(Vector2Int worldCoordinate) {
            return new Vector2Int(GetWorldCoordinateToChunkCoordinate(worldCoordinate.x),
                GetWorldCoordinateToChunkCoordinate(worldCoordinate.y));
        }

        public static Vector2Int GetLocalTileCoordinateToWorldCoordinate(Vector2Int worldCoordinate) {
            return new Vector2Int(GetLocalToWorld(worldCoordinate.x), 
                GetLocalToWorld(worldCoordinate.y));
        }

        public static Vector2Int GetWorldCoordinate(Vector2Int localCoordinate, Vector2Int chunkCoordinate) {

            var worldCoordinate = new Vector2Int(localCoordinate.x + (chunkCoordinate.x * Const.chunkSize),
                localCoordinate.y + (chunkCoordinate.y * Const.chunkSize));
            return worldCoordinate;
        }
        
        private static int GetLocalToWorld(int worldCoordinate) {
            int localCoordinate;
            if (worldCoordinate >= 0) {
                localCoordinate = worldCoordinate % Const.chunkSize;
            }
            else {
                localCoordinate = Const.chunkSize - (Mathf.Abs(worldCoordinate) % Const.chunkSize);
                if (localCoordinate == Const.chunkSize) {
                    localCoordinate = 0;
                }
            }

            return localCoordinate;
        }

        private static int GetWorldCoordinateToChunkCoordinate(int worldCoordinate) {
            int chunkCoordinate;
            if (worldCoordinate >= 0) {
                chunkCoordinate = worldCoordinate / Const.chunkSize;
            }
            else {
                chunkCoordinate = -((Mathf.Abs(worldCoordinate) + Const.chunkSize - 1) / Const.chunkSize);
            }

            return chunkCoordinate;
        }
        
        public static Vector2Int[] GetBuildGridCoordinate(Vector2Int worldCoordinate, Vector2Int size) {
            int firstCoordinateX = worldCoordinate.x - size.x / 2;
            int firstCoordinateY = worldCoordinate.y - size.y / 2;
            Vector2Int[] gridCoordinates = new Vector2Int[size.x * size.y];
            for (int y = 0; y < size.y; y++)
            {
                for (int x = 0; x < size.x; x++) {
                    Vector2Int coordinate = new Vector2Int(firstCoordinateX + x, firstCoordinateY + y);
                    gridCoordinates[x + y * size.x] = coordinate;
                }
            }

            return gridCoordinates;
        }
    }
}
