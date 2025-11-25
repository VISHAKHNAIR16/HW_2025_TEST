namespace DoofusGame
{
    using UnityEngine;
    using System.IO; 

    public class GameSettingsLoader : MonoBehaviour
    {
        public string jsonFilePath = "Assets/Model/doofus_diary.json";
        public static GameSettingsData Settings;

        void Awake()
        {
            if (File.Exists(jsonFilePath))
            {
                string json = File.ReadAllText(jsonFilePath);
                Settings = JsonUtility.FromJson<GameSettingsData>(json);
                Debug.Log("Game settings loaded from JSON.");
            }
            else
            {
                Debug.LogWarning("Game settings JSON not found. Using defaults.");
                Settings = new GameSettingsData();
            }
        }
    }
}
