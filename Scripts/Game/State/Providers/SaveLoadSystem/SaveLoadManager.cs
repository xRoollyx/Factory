
using UnityEngine;

namespace myProject{
    public class SaveLoadManager : MonoBehaviour{
    
        public const string TEST_FILE = "TestFile";

        public GameData LoadWorldData(){
            return SaveLoadSystem.Load(TEST_FILE);
        }

        public void SaveData(){
            //SaveLoadSystem.Save(TEST_FILE, WorldManagers.GameDataManager.GetGameData());
        }

        public void DeleteData(){
            SaveLoadSystem.Delete(TEST_FILE);
        }
    }

    public class GameData{
    }
}