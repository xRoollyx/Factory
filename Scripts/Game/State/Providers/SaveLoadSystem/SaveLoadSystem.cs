using System.IO;
using UnityEngine;

namespace myProject{
    public static class SaveLoadSystem{
        public static void Save(string fileName, GameData data){
            string path = Path.Combine(Application.persistentDataPath + fileName);

            using (FileStream file = new FileStream(path, FileMode.Create)){
                using (StreamWriter stream = new StreamWriter(file)){
                    string json = JsonUtility.ToJson(data);
                    stream.Write(json);
                }
            }
        }

        public static GameData Load(string fileName){
            string path = Path.Combine(Application.persistentDataPath + fileName);

            if (File.Exists(path)){
                using (FileStream file = new FileStream(path, FileMode.Open)){
                    using (StreamReader stream = new StreamReader(file)){
                        string json = stream.ReadToEnd();
                        GameData data = JsonUtility.FromJson<GameData>(json);
                        return data;
                    }
                }
            }

            return null;
        }

        public static void Delete(string fileName){
            string path = Path.Combine(Application.persistentDataPath + fileName);
            if (File.Exists(path)){
                File.Delete(path);
            }
        }
    }
}