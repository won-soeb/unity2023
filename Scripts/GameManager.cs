using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public static GameManager instance;

    public Shooter shooter;
    public Enemy enemy;
    public Item lifeUp;
    public Item timePause;
    [SerializeField] private static int level = 1;
    public int enemyNumber;
    [SerializeField] private int enemyWaves;
    [SerializeField] private float enemyRangeH;
    [SerializeField] private float enemyRangeV;
    [SerializeField] private int enemyDistance;
    [SerializeField] private int itemNumber;
    [SerializeField] private float itemRange;

    public int enemySpeed;
    public int health;
    [HideInInspector] public bool isLoading;
    [HideInInspector] public bool isDead;

    public Text healthText;
    public Text levelText;
    public Text enemyText;
    public Text targetMark;
    public Button resetButton;
    public GameObject gamePanel;
    public GameObject menuPanel;
    public GameObject deadPanel;
    public GameObject clearPanel;

    private void Awake()
    {
        //instance = this;
        Time.timeScale = 1;
        levelText.text = "Level: " + level;
        PlayerPrefs.SetInt("Level", 1);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            StartCoroutine(GameCount());

            for (int i = 0; i < enemyNumber; i++)//Enemy생성
            {
                float randX = Random.Range(-0.1f * enemyRangeH, 0.1f * enemyRangeH);
                float randY = Random.Range(-0.1f * enemyRangeV, 0.1f * enemyRangeV);
                float randZ = Random.Range(-0.1f * enemyRangeH, 0.1f * enemyRangeH);

                Instantiate(enemy, new Vector3(randX, randY, randZ * enemyDistance), Quaternion.identity);
            }
            for (int i = 0; i < itemNumber; i++)//Item생성
            {
                float randX = Random.Range(-0.1f * itemRange, 0.1f * itemRange);
                float randY = Random.Range(-0.1f * itemRange, 0.1f * itemRange);
                float randZ = Random.Range(-0.1f * itemRange, 0.1f * itemRange);

                Instantiate(lifeUp, new Vector3(randX, randY, randZ), Quaternion.identity);
            }
            for (int i = 0; i < itemNumber; i++)//Item생성
            {
                float randX = Random.Range(-0.1f * itemRange, 0.1f * itemRange);
                float randY = Random.Range(-0.1f * itemRange, 0.1f * itemRange);
                float randZ = Random.Range(-0.1f * itemRange, 0.1f * itemRange);

                Instantiate(timePause, new Vector3(randX, randY, randZ), Quaternion.identity);
            }
        }
        else if(SceneManager.GetActiveScene().name == "CountScene")
        {
            //StartCoroutine(GameCount());
        }
    }

    private void Update()
    {
        healthText.text = "Health: " + health;
        enemyText.text = "Enemy: " + (enemyNumber + 1);
        if (health < 0)
            health = 0;
    }

    IEnumerator GameCount()
    {
        isLoading = true;
        targetMark.text = "3";
        yield return new WaitForSeconds(1f);
        targetMark.text = "2";
        yield return new WaitForSeconds(1f);
        targetMark.text = "1";
        yield return new WaitForSeconds(1f);
        targetMark.text = "Start!";
        yield return new WaitForSeconds(0.8f);
        targetMark.text = "+";
        isLoading = false;
    }

    public void ReStartButton()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        gamePanel.SetActive(false);
        menuPanel.SetActive(true);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        gamePanel.SetActive(true);
        menuPanel.SetActive(false);
    }

    public void NextLevel()
    {
        clearPanel.SetActive(false);
        gamePanel.SetActive(true);       
    }

    public void LevelClear()
    {
        gamePanel.SetActive(false);
        clearPanel.SetActive(true);
        PlayerPrefs.SetInt("Level", level + 1);
        PlayerPrefs.Save();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        isDead = true;
        gamePanel.SetActive(false);
        deadPanel.SetActive(true);
    }

    public void LoadMenuScene()
    {
        //Time.timeScale = 1;
        SceneManager.LoadScene("MenuScene");
    }
}
