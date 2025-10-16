using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.State.Buildings;

namespace myProject.Scripts.Game.Gameplay.View.Buildings{
    public class BuildingViewModel{
        private readonly BuildingEntityProxy _buildingEntity;
        private readonly BuildingsService _buildingsService;

        public BuildingViewModel(BuildingEntityProxy buildingEntity, BuildingsService buildingsService){
            _buildingEntity = buildingEntity;
            _buildingsService = buildingsService;
        }
    }
}