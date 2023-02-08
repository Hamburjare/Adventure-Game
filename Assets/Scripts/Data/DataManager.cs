using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public static class DataManager
{
    // Tallentaa tilatiedot JSON tiedostoksi
    public static void SavePlayerDataToJSON(Player player)
    {
        // Luodaan playerData olio
        PlayerData playerData = new PlayerData(player);
        // Muodostetaan tilatiedoista JSON
        string json = JsonUtility.ToJson(playerData);
        // Talletetaan JSON playerData.json nimiseen tiedostoon
        File.WriteAllText($"{Application.persistentDataPath}/playerData.json", json);
    }
    // Hakee tilatiedot JSON tiedostosta
    public static PlayerData LoadPlayerDataFromJSON()
    {
        if(!File.Exists($"{Application.persistentDataPath}/playerData.json") )
        {
            return null;
        }
        // Ladataan JSON tiedosto
        string json = File.ReadAllText($"{Application.persistentDataPath}/playerData.json");
        // Sijoitetaan JSONin sisältäm,tä tilatiedot playerData olioon
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        // Palautetaan playerData olio
        return playerData;
    }
}