using System;
using System.Collections.Generic;

[System.Serializable]

public class GameData
{
    public int playerHealth;
    public int pollitos;
    public string scene;
    public List<Tuple<string, int, string>> items;
    public List<Tuple<string, bool, bool>> doors;
    public List<Tuple<string, bool>> map;

    public GameData(GameManager gameManager, Player player, ItemManager item, List<Tuple<string, bool>> mapData)
    {
        playerHealth = player.life;
        items = item.items;
        pollitos = gameManager.pollitos;
        scene = gameManager.activeScene;
        map = mapData;
        doors = gameManager.doors;
    }

}
