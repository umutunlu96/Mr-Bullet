using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject levelSelection;

    private bool isEscape;

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
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level",1), LoadSceneMode.Single);
    }


    public void LevelSelect()
    {
        play.SetActive(false);
        levelSelection.SetActive(true);
    }
}
