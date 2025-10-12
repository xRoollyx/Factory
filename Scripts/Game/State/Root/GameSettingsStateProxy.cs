using R3;

namespace myProject.Scripts.Game.State.Root{
    public class GameSettingsStateProxy{
        public ReactiveProperty<int> SoundVolume { get; }
        public ReactiveProperty<int> MusicVolume { get; }

        public GameSettingsStateProxy(GameSettingsState gameSettingsState){
            SoundVolume = new ReactiveProperty<int>(gameSettingsState.SoundVolume);
            MusicVolume = new ReactiveProperty<int>(gameSettingsState.MusicVolume);
            
            MusicVolume.Subscribe(value => gameSettingsState.MusicVolume = value);
            SoundVolume.Subscribe(value => gameSettingsState.SoundVolume = value);
        }
    }
}