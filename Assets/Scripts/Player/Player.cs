using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // SIngelton
    private static Player instance;

    // Pelihahmon tilatiedot

    public int Health
    {
        get => GameManager.s_heroHealth;
    }

    public int Lvl
    {
        get => GameManager.s_lvl;
    }

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError(message: "Player on tyhjä!");
            }
            return instance;
        }
    }

    // Referenssi pelaajan lempinimeen GUIssa
    [SerializeField]
    private TMP_Text playerNickname;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        // playerNickname.text = Login.nickname;
        LoadPlayerDataFromJSON();
        GameManager.Instance.UpdateHealthUI();
        GameManager.Instance.UpdateLvlUI();
    }

    private void LoadPlayerDataFromJSON()
    {
        PlayerData playerData = DataManager.LoadPlayerDataFromJSON();
        if (playerData == null)
        {
            SavePlayerDataToJSON();
            return;
        }
        // Päivitetään playerData oliota saadut pelihahmon tilatiedot
        GameManager.s_heroHealth = playerData.health;
        GameManager.s_lvl = playerData.lvl;
        Vector3 position = new Vector3()
        {
            x = playerData.position[0],
            y = playerData.position[1],
            z = playerData.position[2]
        };
        // Sijoittaa pelihahmon JSON tiedostossa olevien x,y,z arvojen kertomaan paikkaan
        transform.position = position - new Vector3(1f, 0, 1f);
    }

    // Tallennetaan pelihahmon tilatiedot JSON tiedostoksi
    public void SavePlayerDataToJSON()
    {
        //print("Tallennus käynnissä...");
        DataManager.SavePlayerDataToJSON(this);
    }
}
