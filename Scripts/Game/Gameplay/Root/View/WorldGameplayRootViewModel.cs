using myProject.Scripts.Game.Gameplay.Services;
using myProject.Scripts.Game.Gameplay.View.Buildings;
using ObservableCollections;

namespace myProject.Scripts.Game.Gameplay.Root.View{
    public class WorldGameplayRootViewModel{

        public readonly IObservableCollection<BuildingViewModel> AllBuildings;

        public WorldGameplayRootViewModel(BuildingsService buildingsService){
            AllBuildings = buildingsService.AllBuildings;
        }
    }
}