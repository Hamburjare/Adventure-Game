using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject hero;

    [SerializeField]
    public GameObject spider;

    public static int s_lvl;

    [SerializeField]
    int m_heroMaxHealth = 5;

    public static int s_heroMaxHealth { get; private set; }

    [SerializeField]
    int m_spiderMaxHealth = 20;

    [SerializeField]
    public static bool s_isSpiderDead = false;

    public static int s_heroHealth = 5;

    public static bool s_isHeroDead = false;

    [SerializeField]
    int m_spiderHealth;

    [SerializeField]
    TextMeshProUGUI m_lvlText;

    [SerializeField]
    List<GameObject> heroHealthUI = new List<GameObject>();

    [SerializeField]
    List<GameObject> m_winPanel = new List<GameObject>();

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        m_spiderHealth = m_spiderMaxHealth;
    }


    // Pelaajan terveyspisteiden muuttaminen UIssa
    public void UpdateHealthUI()
    {
        // Jos pelaajan terveyspisteet ovat pienemmät kuin 0, niin asetetaan ne 0:ksi
        if (s_heroHealth <= 0 && !s_isHeroDead)
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            s_heroHealth = 0;
            AudioManager.instance.Play("GameOver");
            HeroDead();
        }

        // Jos pelaajan terveyspisteet ovat suuremmat kuin 5, niin asetetaan ne 5:ksi
        if (s_heroHealth > m_heroMaxHealth)
            s_heroHealth = m_heroMaxHealth;

        // Jos pelaajan terveyspisteet ovat pienemmät kuin 5, niin piilotetaan ylimääräiset terveyspisteet
        if (s_heroHealth < m_heroMaxHealth)
        {
            for (int i = s_heroHealth; i < heroHealthUI.Count; i++)
            {
                heroHealthUI[i].SetActive(false);
            }
        }
        // Jos pelaajan terveyspisteet ovat suuremmat kuin 0, niin näytetään terveyspisteet
        if (s_heroHealth > 0)
        {
            for (int i = 0; i < s_heroHealth; i++)
            {
                heroHealthUI[i].SetActive(true);
            }
        }
    }

    public void UpdateLvlUI()
    {
        m_lvlText.text = $"XP: {s_lvl.ToString()}";
    }

    // Pelaajan terveyspisteiden lisääminen
    public void AddHeroHealth(int amount)
    {
        s_heroHealth += amount;
        UpdateHealthUI();
    }

    // Pelaajan terveyspisteiden vähentäminen
    public void RemoveHeroHealth(int amount)
    {
        s_heroHealth -= amount;
        UpdateHealthUI();
    }

    public void RemoveSpiderHealth(int amount)
    {
        m_spiderHealth -= amount;
        if (m_spiderHealth <= 0 && !s_isSpiderDead)
        {
            SpiderDead();
        }
    }

    void HeroDead()
    {
        s_isHeroDead = true;
        hero.GetComponent<Animator>().SetTrigger("Die");
        m_winPanel[0].SetActive(true);
        s_heroHealth = 0;
    }

    void SpiderDead()
    {
        s_isSpiderDead = true;
        spider.GetComponent<Animator>().enabled = false;
        m_winPanel[1].SetActive(true);
        m_spiderHealth = 0;
        AudioManager.instance.Play("WinFanfare");
    }

    // Pelaajan kokemuspisteiden muuttaminen
    public void AddXp(int amount)
    {
        s_lvl += amount;
        UpdateLvlUI();
    }
}
