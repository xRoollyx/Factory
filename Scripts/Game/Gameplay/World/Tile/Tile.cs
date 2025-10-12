using UnityEngine;
using UnityEngine.Events;

namespace myProject{
    public class Tile{
        public readonly UnityEvent<int> onTileTypeChanged = new();
        public TileData data{ get; }
        public Vector2Int worldCoordinate{ get; }
        public Vector2Int localCoordinate{ get; }
        public bool isEmpty => _createdBuild == null;
        private BaseBuilding _createdBuild;

        public Tile(TileData data){
            this.data = data;
            type = data.tileType;
            worldCoordinate = data.worldCoordinate;
            localCoordinate = data.localCoordinate;
        }

        public TileType type{
            get => data.tileType;
            set{
                var oldType = data.tileType;
                if (oldType == value){
                    return;
                }

                data.tileType = value;
                onTileTypeChanged?.Invoke((int)type);
            }
        }


        private readonly Tile[] _neighbors = new Tile[8];

        public Tile GetNeighbor(Direction direction){
            return _neighbors[(int)direction];
        }

        public Tile[] GetNeighbors(){
            return _neighbors;
        }

        public void SetNeighbor(Direction direction, Tile tile){
            _neighbors[(int)direction] = tile;
            tile._neighbors[(int)direction.Opposite()] = this;
        }

        public void BuildBuilding(BaseBuilding baseBuilding){
            if (isEmpty){
                _createdBuild = baseBuilding;
            }
        }

        public void DestroyBuilding(){
            if (!isEmpty){
                _createdBuild = null;
            }
        }

        public void Interact(){
            _createdBuild?.Interact();
            //_createdBuild?.Destroy();
        }
    }
}