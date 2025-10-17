using System.Threading.Tasks;

namespace myProject.Scripts.Game.Settings{
    public interface ISettingsProvider{
        GameSettings GameSettings { get; }
        ApplicationSettings ApplicationSettings { get; }
        
        Task<GameSettings> LoadGameSettings();
    }
}