using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static bool isNewGame;
    public static string scene;

    public static void SaveGameData(GameManager gameManager, Player player, ItemManager item, List<Tuple<string, bool>> map)
    {
        GameData gameData = new GameData(gameManager, player, item, map);
        string dataPath = Application.persistentDataPath + "/game.save";
        
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, gameData);
        fileStream.Close();
    }

    public static GameData LoadGameData()
    {
        string dataPath = Application.persistentDataPath + "/game.save";
        
        if (File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            GameData gameData = (GameData)binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return gameData;

        }
        else
            return null;
    }
}
