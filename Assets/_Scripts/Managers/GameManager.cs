using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Fade anim kapatildi.


public class GameManager : MonoBehaviour
{
    public int enemyCount = 1;

    [HideInInspector]
    public bool gameOver, isGoal;

    public int blackBalls = 3;
    public int goldenBalls = 1;

    public GameObject blackBall, goldenBall;

    private int levelNumber;


    //private Animator fadeAnim;

    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level",1);

        //fadeAnim = GameObject.Find("Fade").GetComponent<Animator>();

        FindObjectOfType<PlayerController>().ammo = blackBalls + goldenBalls;

        for (int i = 0; i < blackBalls; i++)
        {
            GameObject bbTemp = Instantiate(blackBall);
            bbTemp.transform.SetParent(GameObject.Find("Bullets").transform);
        }

        for (int i = 0; i < goldenBalls; i++)
        {
            GameObject gbTemp = Instantiate(goldenBall);
            gbTemp.transform.SetParent(GameObject.Find("Bullets").transform);
        }

    }

    void Update()
    {
        if (!gameOver && isGoal)
        {
            GameUI.instance.WinScreen();
            if (levelNumber >= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level", levelNumber + 1);
            }

            gameOver = true;
        }

        if (!gameOver && FindObjectOfType<PlayerController>().ammo <= 0)
        {
            gameOver = true;
            GameUI.instance.GameOverScreen();
        }


        //if (!gameOver && FindObjectOfType<PlayerController>().ammo <= 0 && enemyCount > 0 && 
        //    GameObject.FindGameObjectsWithTag("Bullet").Length <= 0)
        //{
        //    gameOver = true;
        //    GameUI.instance.GameOverScreen();
        //}
    }

    public void CheckBalls()
    {
        if (goldenBalls > 0)
        {
            goldenBalls--;
            GameObject.FindGameObjectWithTag("GoldenBullet").SetActive(false);
        }
        else if (blackBalls > 0)
        {
            blackBalls--;
            GameObject.FindGameObjectWithTag("BlackBullet").SetActive(false);
        }
    }

    public void CheckEnemyCount()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyCount <= 0)
        {
            GameUI.instance.WinScreen();
            if (levelNumber >= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level",levelNumber + 1);
            }
        }
    }

    //IEnumerator FadeIn(int sceneIndex)
    //{
    //    fadeAnim.SetTrigger("End");
    //    yield return new WaitForSeconds(1);
    //    SceneManager.LoadScene(sceneIndex);
    //}

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex));
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //StartCoroutine(FadeIn(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
        //StartCoroutine(FadeIn(0));
    }
}
