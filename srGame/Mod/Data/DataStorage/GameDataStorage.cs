using System;
using LitJson;
using System.IO;

public class GameDataStorage
{
    private static readonly string FilePath = "game_data.json";
    private static GameDataStorage gi;
   
    public static bool dapdo = false;
    public static GameDataStorage gI()
    {
        if (gi == null)
        {
            gi = new GameDataStorage();
        }
        return gi;
    }

    public void SaveGameData(GameData data)
    {
        string jsonData = JsonMapper.ToJson(data);
        File.WriteAllText(FilePath, jsonData);
    }

    public GameData LoadGameData()
    {
        if (File.Exists(FilePath))
        {
            string jsonData = File.ReadAllText(FilePath);
            return JsonMapper.ToObject<GameData>(jsonData);
        }
        else
        {
            GameData defaultData = new GameData();

            defaultData.Client.SpeedGame = 5f;
            defaultData.Client.ipServer = null;
            defaultData.Client.width = 1024;
            defaultData.Client.height = 600;

            SaveGameData(defaultData);

            return new GameData();
        }
    }
}