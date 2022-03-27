using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject levelSelection;
    public GameObject main;

    public int maxLevelCount;
    private bool isEscape;

    private void Start()
    {
        PlayerPrefs.SetInt("AdShowCount", 0);
    }

    private void Update()
    {
        CloseApplication();
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
                ReturnMain();
                if (!IsInvoking("DisableDoubleClick"))
                    Invoke("DisableDoubleClick", 0.3f);
            }
        }
    }

    void DisableDoubleClick()
    {
        isEscape = false;
    }

    public void PlayGame()
    {
        if (PlayerPrefs.GetInt("Level", 1) >= maxLevelCount)
            SceneManager.LoadScene(maxLevelCount, LoadSceneMode.Single);
        else
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1), LoadSceneMode.Single);
    }


    public void LevelSelect()
    {
        play.SetActive(false);
        levelSelection.SetActive(true);
        main.SetActive(false);
    }

    public void ReturnMain()
    {
        play.SetActive(true);
        levelSelection.SetActive(false);
        main.SetActive(true);
    }


}
