[System.Serializable]

public class PlayerData
{
    public int chocolateBar;

    public PlayerData(Player player)
    {
        chocolateBar = player.chocolateBar;
    }
}
