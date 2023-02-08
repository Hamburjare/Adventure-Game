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
    [SerializeField]
    private int m_health;

    public int Health
    {
        get => m_health;
    }

    [SerializeField]
    private int m_lvl;

    public int Lvl
    {
        get => m_lvl;
    }

    [SerializeField]
    List<GameObject> m_healthUI = new List<GameObject>();

    [SerializeField]
    TextMeshProUGUI m_lvlText;

    [SerializeField]
    List<GameObject> m_winPanel = new List<GameObject>();

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
        UpdateHealthUI();
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
        m_health = playerData.health;
        m_lvl = playerData.lvl;
        Vector3 position = new Vector3()
        {
            x = playerData.position[0],
            y = playerData.position[1],
            z = playerData.position[2]
        };
        // Sijoittaa pelihahmon JSON tiedostossa olevien x,y,z arvojen kertomaan paikkaan
        transform.position = position;

        m_lvlText.text = $"XP: {m_lvl.ToString()}";
    }



    // Tallennetaan pelihahmon tilatiedot JSON tiedostoksi
    public void SavePlayerDataToJSON()
    {
        //print("Tallennus käynnissä...");
        DataManager.SavePlayerDataToJSON(this);
    }

    // Pelaajan terveyspisteiden muuttaminen UIssa
    public void UpdateHealthUI()
    {
        // Jos pelaajan terveyspisteet ovat pienemmät kuin 0, niin asetetaan ne 0:ksi
        if (m_health <= 0)
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            m_winPanel[0].SetActive(true);
            m_health = 0;
        }

        // Jos pelaajan terveyspisteet ovat suuremmat kuin 5, niin asetetaan ne 5:ksi
        if (m_health > 5)
            m_health = 5;

        // Jos pelaajan terveyspisteet ovat pienemmät kuin 5, niin piilotetaan ylimääräiset terveyspisteet
        if (m_health < 5)
        {
            for (int i = m_health; i < m_healthUI.Count; i++)
            {
                m_healthUI[i].SetActive(false);
            }
        }
        // Jos pelaajan terveyspisteet ovat suuremmat kuin 0, niin näytetään terveyspisteet
        if (m_health > 0)
        {
            for (int i = 0; i < m_health; i++)
            {
                m_healthUI[i].SetActive(true);
            }
        }
    }

    // Pelaajan terveyspisteiden lisääminen
    public void AddHealth(int amount)
    {
        m_health += amount;
        UpdateHealthUI();
    }

    // Pelaajan terveyspisteiden vähentäminen
    public void RemoveHealth(int amount)
    {
        m_health -= amount;
        UpdateHealthUI();
    }

    // Pelaajan kokemuspisteiden muuttaminen
    public void AddXp(int amount)
    {
        m_lvl += amount;
        m_lvlText.text = $"XP: {m_lvl.ToString()}";
    }
}
