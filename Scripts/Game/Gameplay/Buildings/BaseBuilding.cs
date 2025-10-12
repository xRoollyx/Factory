using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace myProject{
    public abstract class BaseBuilding : IBuilding{

        private List<Tile> _tiles = new List<Tile>();
        protected BuildManager _buildManager;
        
        public ReactiveProperty<Vector2Int> position = new ReactiveProperty<Vector2Int>();
        public Subject<Unit> destroyBuilding  = new Subject<Unit>();
        public ReactiveProperty<Color> color = new ReactiveProperty<Color>();
        
        public BuildingData data {get; set;}
        public int buildId{ get; }
        

        public BuildingsSo buildingsSo{ get; private set; }

        protected BaseBuilding(BuildManager buildManager, BuildingsSo buildingsSo, int buildId){
            _buildManager = buildManager;
            this.buildingsSo = buildingsSo;
            this.buildId = buildId;
        }

        public virtual void Update(float deltaTime){}

        public virtual void Interact(){
        }

        public void SetTile(Tile tile){
            _tiles.Add(tile);
        }

        public void Destroy(){
            foreach (var tile in _tiles){
                tile.DestroyBuilding();
            }
            _tiles.Clear();
            _buildManager.DestroyBuilding(this);
            destroyBuilding.OnNext(Unit.Default);
        }

        
    }
}