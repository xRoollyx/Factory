using System.Collections.Generic;
using myProject.Scripts.Game.State.Maps;
using myProject.Scripts.Game.State.Providers;
using R3;
using UnityEngine;

namespace myProject.Scripts.Game.State.Root.Providers{
    public class PlayerPrefsGameStateProvider: IGameStateProvider{
        private const string GAME_STATE_KEY = nameof(GAME_STATE_KEY);
        private const string GAME_SETTINGS_STATE_KEY = nameof(GAME_SETTINGS_STATE_KEY);
        
        
        public GameStateProxy GameState{ get; private set; }
        public GameSettingsStateProxy SettingsState{ get; private set; }
        
        private GameState _gameStateOrigin;
        private GameSettingsState  _gameSettingsOrigin;
        
        
        public Observable<GameStateProxy> LoadGameState(){
            if (!PlayerPrefs.HasKey(GAME_STATE_KEY)){

                GameState = CreateGameStateFromSettings();
                Debug.Log("GameState created from settings" + JsonUtility.ToJson(_gameStateOrigin, true));
                
                SaveGameState();
            }
            else{
                var json = PlayerPrefs.GetString(GAME_STATE_KEY);
                _gameStateOrigin = JsonUtility.FromJson<GameState>(json);
                GameState = new GameStateProxy(_gameStateOrigin);

                Debug.Log("Game State Loaded: " + json);
            }

            return Observable.Return(GameState);
        }

        public Observable<GameSettingsStateProxy> LoadSettingsState(){
            if (!PlayerPrefs.HasKey(GAME_SETTINGS_STATE_KEY)){
                SettingsState = CreateGameSettingsFromSettings();
                SaveSettingsState();
            }else{
                var json = PlayerPrefs.GetString(GAME_SETTINGS_STATE_KEY);
                _gameSettingsOrigin = JsonUtility.FromJson<GameSettingsState>(json);
                SettingsState = new GameSettingsStateProxy(_gameSettingsOrigin);
            }
            return Observable.Return(SettingsState);
        }

        public Observable<bool> SaveGameState(){
            var json = JsonUtility.ToJson(_gameStateOrigin, true);
            PlayerPrefs.SetString(GAME_STATE_KEY,json);

            return Observable.Return(true);
        }
        
        public Observable<bool> SaveSettingsState(){
            var json = JsonUtility.ToJson(_gameSettingsOrigin, true);
            PlayerPrefs.SetString(GAME_SETTINGS_STATE_KEY, json);
            
            return Observable.Return(true);
        }

        public Observable<bool> ResetGameState(){
            GameState = CreateGameStateFromSettings();
            SaveGameState();
            
            return Observable.Return(true);
        }

        public Observable<bool> ResetSettingsState(){
            SettingsState = CreateGameSettingsFromSettings();
            SaveSettingsState();
            
            return Observable.Return(true);
        }
        
        private GameStateProxy CreateGameStateFromSettings(){
            _gameStateOrigin = new GameState{
                Maps = new List<MapState>()
            };
            return new GameStateProxy(_gameStateOrigin);
        }
        private GameSettingsStateProxy CreateGameSettingsFromSettings(){
            _gameSettingsOrigin = new GameSettingsState{
                MusicVolume = 8,
                SoundVolume = 8,
            };
            return new GameSettingsStateProxy(_gameSettingsOrigin);
        }
    }
}
