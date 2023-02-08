using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    // Talletettavat tilatiedot
    public float[] position;
    public int health;

    public int lvl;
    
    // Konstruktori
    public PlayerData(Player player)
    {
        health = player.Health;
        lvl = player.Lvl;
        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }
}