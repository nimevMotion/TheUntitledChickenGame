using System;
using System.Collections.Generic;

[System.Serializable]

public class GameData
{
    public int playerHealth;
    public float playerPosX;
    public float playerPosY;
    public float playerPosZ;
    public string scene;
    public List<Tuple<string, int, string>> items;
    public List<Tuple<string, bool, bool>> doors;
    public List<Tuple<string, bool>> map;
    public List<Tuple<string, bool>> pollitos;

    public GameData(GameManager gameManager, Player player, ItemManager item, List<Tuple<string, bool>> mapData)
    {
        playerHealth = player.life;
        playerPosX = gameManager.savePoint.x;
        playerPosY = gameManager.savePoint.y;
        playerPosZ = gameManager.savePoint.z;
        items = item.items;
        pollitos = gameManager.pollitos;
        scene = gameManager.activeScene;
        map = mapData;
        doors = gameManager.doors;
    }

}
