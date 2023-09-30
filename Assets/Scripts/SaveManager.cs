
using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static void SavePlayerData(Player player)
    {
        PlayerData playerData = new PlayerData(player);
        string dataPath = Application.persistentDataPath + "/player.save";
        FileStream fileStream = new FileStream(dataPath, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(fileStream, playerData);
        fileStream.Close();
    }

    public static PlayerData LoadPlayerData()
    {
        string dataPath = Application.persistentDataPath + "/player.save";

        if(File.Exists(dataPath))
        {
            FileStream fileStream = new FileStream(dataPath, FileMode.Open);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            PlayerData playerData = (PlayerData) binaryFormatter.Deserialize(fileStream);
            fileStream.Close();
            return playerData;
 
        }else
            return null;
    }

    public static void SaveGameData(GameManager gameManager, Player player, ItemManager item, List<Tuple<string, bool>> map)
    {
        GameData gameData = new GameData(gameManager, player, item, map);
        //PlayerData playerData = new PlayerData();
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
