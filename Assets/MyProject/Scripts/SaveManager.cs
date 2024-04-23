using System;
using UnityEngine;

public class SaveManager
{
    internal void RestorePlayer(Player player)
    {
        if (PlayerPrefs.HasKey("player") == false) return;

        string json = PlayerPrefs.GetString("player");

        var data = JsonUtility.FromJson<PlayerData>(json);

        player.Restore(data);
    }

    internal void SavePlayer(Player player)
    {
        var data = player.Save();
        string json = JsonUtility.ToJson(data);
        Debug.Log(json);
        PlayerPrefs.SetString("player", json);
    }
}
