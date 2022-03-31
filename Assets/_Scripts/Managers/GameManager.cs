using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int kaleCount = 1;
    
    [HideInInspector]
    public bool gameOver, gameWin;

    public int blackBalls = 3;
    public int goldenBalls = 1;

    public GameObject blackBall, goldenBall;

    private int levelNumber;
    private bool isEscape;

    private Animator fadeAnim;


    [Header("ADS")]
    private bool SHOWADS = false;
    public int adShowCount; //Ads
    public bool adShow; //Ads
    public bool rewardRequest;  //Ads

    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level",1);
        adShowCount = PlayerPrefs.GetInt("AdShowCount", 0);

        fadeAnim = GameObject.Find("Fade").GetComponent<Animator>();

        FindObjectOfType<PlayerController>().ammo = blackBalls + goldenBalls;

        for (int i = 0; i < blackBalls; i++)
        {
            GameObject bbTemp = Instantiate(blackBall);
            bbTemp.transform.SetParent(GameObject.Find("Balls").transform);
            bbTemp.transform.localScale = new Vector3(1, 1, 1);
        }

        for (int i = 0; i < goldenBalls; i++)
        {
            GameObject gbTemp = Instantiate(goldenBall);
            gbTemp.transform.SetParent(GameObject.Find("Balls").transform);
            gbTemp.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void Start()
    {
        AdCheck(SHOWADS);
    }

    void Update()
    {
        if (!gameOver && FindObjectOfType<PlayerController>().ammo <= 0 && kaleCount > 0
            && GameObject.FindGameObjectsWithTag("Ball").Length <= 0)
        {
            gameOver = true;
            GameUI.instance.GameOverScreen();

            ShowAd(adShow);
            RewardRequest(rewardRequest);
        }

        if (gameWin && GameObject.FindGameObjectsWithTag("Ball").Length <= 0)
        {
            GameUI.instance.WinScreen();
            if (levelNumber >= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level", levelNumber + 1);
            }

            ShowAd(adShow);

        }
        CloseApplication();
    }

    public void CheckBalls()
    {
        if (goldenBalls > 0)
        {
            goldenBalls--;
            GameObject.FindGameObjectWithTag("GoldenBall").SetActive(false);
        }
        else if (blackBalls > 0)
        {
            blackBalls--;
            GameObject.FindGameObjectWithTag("BlackBall").SetActive(false);
        }
    }

    public void CheckKaleCount()
    {
        kaleCount = GameObject.FindGameObjectsWithTag("Kale").Length;

        if (kaleCount <= 0)
        {
            gameWin = true;
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
        yield return new WaitForSeconds(.5f);
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
                StartCoroutine(FadeIn(0));
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
