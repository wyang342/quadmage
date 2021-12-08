using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Variables to Save, playerSaveData
    public int highScore;
    public int coins;

    public void SavePlayer()
    {
        try
        {
            SaveSystem.SavePlayer(this);
        }
        catch (Exception e)
        {
            Debug.Log("Can't save player because " + e);
        }
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        try
        {
            highScore = data.highScore;
            coins = data.coins;
        }
        catch (Exception e)
        {
            Debug.Log("Can't load player because " + e);
        }
    }
}