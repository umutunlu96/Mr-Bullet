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

    void Awake()
    {
        levelNumber = PlayerPrefs.GetInt("Level",1);

        //fadeAnim = GameObject.Find("Fade").GetComponent<Animator>();

        FindObjectOfType<PlayerController>().ammo = blackShirkens + goldenShirkens;

        for (int i = 0; i < blackShirkens; i++)
        {
            GameObject blackShirken = Instantiate(this.blackShirken);
            blackShirken.transform.SetParent(GameObject.Find("Shirkens").transform);
            blackShirken.transform.rotation = Quaternion.Euler(0,0,-30);
        }

        for (int i = 0; i < goldenShirkens; i++)
        {
            GameObject goldenShirken = Instantiate(this.goldenShirken);
            goldenShirken.transform.SetParent(GameObject.Find("Shirkens").transform);
            goldenShirken.transform.rotation = Quaternion.Euler(0, 0, -30);
        }

    }

    void Update()
    {
        if (!gameOver && FindObjectOfType<PlayerController>().ammo <= 0 && enemyCount > 0 && 
            GameObject.FindGameObjectsWithTag("Shirken").Length <= 0)
        {
            gameOver = true;
            GameUI.instance.GameOverScreen();
        }
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
            if (levelNumber >= SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level",levelNumber + 1);
            }
        }
    }

    IEnumerator FadeIn(int sceneIndex)
    {
        fadeAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }

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
