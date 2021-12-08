[System.Serializable]
public class PlayerData
{
    public int coins;
    public int highScore;

    public PlayerData (Player player)
    {
        coins = player.coins;
        highScore = player.highScore;
    }
}