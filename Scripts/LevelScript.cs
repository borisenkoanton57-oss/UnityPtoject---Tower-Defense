using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform[] pages;
    [SerializeField] private Transform books;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text levelText;

    [Header("Stats")]
    public int health;
    public int level;
    public int score;
    private int countofEnemy = 10;

    [Header("Path")]
    public Vector3 spawn;
    public Transform paths;

    [Header("Prefabs")]
    [SerializeField] private EnemyData[] enemies;
    [SerializeField] private TurretData[] turrets;

    [Header("Turret")]
    [SerializeField] private BuildingPlacer placer;

    [Header("Damage Effect")]
    [SerializeField] private DamageEffect damageEffect;

    [Header("Victory")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private AudioSource victoryAudio;

    
    void Start()
    {
        FillTurretIcons();
        FillScoreText(0);
        Invoke("GenerateEnemy", 5f);
    }

    
    void Update()
    {
        
    }

    private void FillTurretIcons()
    {
        int currentIndex = 0;
        for(int i = 0; i < pages.Length; i++)
        {
             for (int j = 0; j < pages[i].childCount; j++)
            {
                if(currentIndex < turrets.Length && turrets[currentIndex].Sprite)
                {
                    pages[i].GetChild(j).GetChild(0).
                    GetComponent<Image>().sprite = turrets[currentIndex].Sprite;
                    pages[i].GetChild(j).GetChild(1).
                    GetComponent<TMP_Text>().text = turrets[currentIndex].Price.ToString();
                    pages[i].GetChild(j).GetChild(2).
                    GetComponent<TMP_Text>().text = turrets[currentIndex].TurretName;
                }
                currentIndex++;
            }
        }
    }

    public void BuyTurret(int turretIndex)
    {
        if(turretIndex < turrets.Length && score >= turrets[turretIndex].Price && turrets[turretIndex].Model)
        {
            placer.buildingPrefab = turrets[turretIndex].Model;
            placer.price = -turrets[turretIndex].Price;
            placer.needToBuild = true;

            CustomCursor cursor = FindFirstObjectByType<CustomCursor>();

            if (cursor != null)
            {
                cursor.SetBuildCursor(true);
            }
            
            placer.placerPrefab = turrets[turretIndex].TurretPlacer;
        }
    }

    public void FillScoreText(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }

    public void DamageFinish()
    {
        health--;

        if (damageEffect != null)
        {
            damageEffect.PlayDamageEffect();
        }

        if(books.childCount > 0)
        {
            Destroy(books.GetChild(0).gameObject);
        }

        if (health <= 0)
        {
            GameTowerMenu gameMenu = FindFirstObjectByType<GameTowerMenu>();

            if (gameMenu != null)
            {
                gameMenu.ShowDefeat();
            }
        }
    }

    public void Victory()
    {
        GameTowerMenu.gameFinished = true;

        victoryPanel.SetActive(true);

        Time.timeScale = 0f;

        if (victoryAudio != null)
        {
            victoryAudio.Play();
        }
    }

    public void EndGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void GenerateEnemy()
    {
        EnemyScript currentEnemy = Instantiate(enemies[level / 5].Model, 
        spawn, Quaternion.identity, transform.GetChild(0)).
        GetComponent<EnemyScript>();
        currentEnemy.paths = paths;
        currentEnemy.level = this;
        countofEnemy--;

        if (countofEnemy > 0)
        {
            Invoke("GenerateEnemy", 2f);
        }
        else
        {
            countofEnemy = 10 + level;
        }
    }

    public void ChangeLevel()
    {
        level++;
        levelText.text = level.ToString();
        Invoke("GenerateEnemy", 5f);
    }
}
