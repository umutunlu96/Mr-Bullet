using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Fade animler kapatildi.


public class GameManager : MonoBehaviour
{
    public int enemyCount = 1;
    [HideInInspector]
    public bool gameOver;
    public int blackShirkens = 3;
    public int goldenShirkens = 1;

    public GameObject blackShirken, goldenShirken;

    private int levelNumber;

    private Animator fadeAnim;

    private bool isEscape;

    [Header("ADS")]
    private bool SHOWADS = true;
    public int adShowCount; //Ads
    public bool adShow; //Ads
    public bool rewardRequest;



    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level",1);

        adShowCount = PlayerPrefs.GetInt("AdShowCount", 0);

        fadeAnim = GameObject.Find("Fade").GetComponent<Animator>();

        FindObjectOfType<PlayerController>().ammo = blackShirkens + goldenShirkens;

        for (int i = 0; i < blackShirkens; i++)
        {
            GameObject blackShirken = Instantiate(this.blackShirken);
            blackShirken.transform.SetParent(GameObject.Find("Shirkens").transform);
            blackShirken.transform.rotation = Quaternion.Euler(0,0,-30);
            blackShirken.transform.localScale = new Vector3(1, 1, 1);
        }

        for (int i = 0; i < goldenShirkens; i++)
        {
            GameObject goldenShirken = Instantiate(this.goldenShirken);
            goldenShirken.transform.SetParent(GameObject.Find("Shirkens").transform);
            goldenShirken.transform.rotation = Quaternion.Euler(0, 0, -30);
            goldenShirken.transform.localScale = new Vector3(1, 1, 1);
        }

    }

    private void Start()
    {
        AdCheck(SHOWADS);
    }

    void Update()
    {
        if (!gameOver && FindObjectOfType<PlayerController>().ammo <= 0 && enemyCount > 0 && 
            GameObject.FindGameObjectsWithTag("Shirken").Length <= 0)
        {
            gameOver = true;
            GameUI.instance.GameOverScreen();

            ShowAd(adShow);
            RewardRequest(rewardRequest);
        }

        CloseApplication();

    }

    public void CheckBullets()
    {
        if (goldenShirkens > 0)
        {
            goldenShirkens--;
            GameObject.FindGameObjectWithTag("GoldenShirken").SetActive(false);
        }
        else if (blackShirkens > 0)
        {
            blackShirkens--;
            GameObject.FindGameObjectWithTag("BlackShirken").SetActive(false);
        }
    }

    public void CheckEnemyCount()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount <= 0)
        {
            GameUI.instance.WinScreen();
            if (levelNumber == SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level",levelNumber + 1);
            }

            ShowAd(adShow);
        }
    }

    private void AdCheck(bool showAds)
    {
        if (showAds)
        {
            adShowCount = PlayerPrefs.GetInt("AdShowCount", 0);
            rewardRequest = true;
            if (adShowCount >= 5)
            {
                AdManager.instance.RequestIntertial();
                adShow = true;
            }
        }
    }

    private void ShowAd(bool adShow)
    {
        if (adShow)
        {
            AdManager.instance.ShowIntertial();
            this.adShow = false;
        }
    }
    private void RewardRequest(bool rewardRequest)
    {
        if (rewardRequest)
        {
            AdManager.instance.RequestRewarded();
            this.rewardRequest = false;
        }
    }

    public void RewardShow()
    {
        AdManager.instance.ShowRewarded();
    }


    IEnumerator FadeIn(int sceneIndex)
    {
        fadeAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }

    public void Restart()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex));
        PlayerPrefs.SetInt("AdShowCount", PlayerPrefs.GetInt("AdShowCount") + 1);
    }

    public void NextLevel()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex + 1));
        PlayerPrefs.SetInt("AdShowCount", PlayerPrefs.GetInt("AdShowCount") + 1);
    }

    public void Exit()
    {
        //SceneManager.LoadScene("MainMenu");
        StartCoroutine(FadeIn(0));
    }

    void CloseApplication()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (isEscape)
            {
                Application.Quit();
            }
            else
            {
                isEscape = true;
                if (!IsInvoking("DisableDoubleClick"))
                    Invoke("DisableDoubleClick", 0.3f);
            }
        }
    }


    void DisableDoubleClick()
    {
        isEscape = false;
    }
}
