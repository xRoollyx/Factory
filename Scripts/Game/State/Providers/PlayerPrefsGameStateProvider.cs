using UnityEngine;

namespace myProject{
    public class PlayerPrefsGameStateProvider: IGameStateProvider{
        private const string KEY_PREFS_STATE = nameof(KEY_PREFS_STATE);
        
        
        public GameState gameStateOrigin{ get; private set; }
        
        
        public GameState LoadGameState(){
            if (!PlayerPrefs.HasKey(KEY_PREFS_STATE)){
                
                Debug.Log("GameState created from settings" + JsonUtility.ToJson(gameStateOrigin, true));
                
                SaveGameState();
            }
            else{
                var json = PlayerPrefs.GetString(KEY_PREFS_STATE);
                gameStateOrigin = JsonUtility.FromJson<GameState>(json);

                Debug.Log("Game State Loaded: " + json);
            }

            return gameStateOrigin;
        }


        public bool SaveGameState(){
            var json = JsonUtility.ToJson(gameStateOrigin, true);
            PlayerPrefs.SetString(KEY_PREFS_STATE,json);

            return true;
        }

        public bool ResetGameState(){
            return true;
        }
    }
}
