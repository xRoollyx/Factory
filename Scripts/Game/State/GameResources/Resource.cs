using R3;

namespace myProject.Scripts.Game.State.GameResources{
    public class Resource{
        public readonly ResourceData Original;
        public ResourceType ResourceType => Original.ResourceType;
        public readonly ReactiveProperty<int> Amount;

        public Resource(ResourceData original){
            Original = original;
            Amount = new ReactiveProperty<int>(original.Amount);
            
            Amount.Subscribe(newValue => {
                original.Amount = newValue;
            });
        }
    }
}