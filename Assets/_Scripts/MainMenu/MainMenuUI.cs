using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject levelSelection;
    public GameObject ninjaMainMenu;

    private bool isEscape;

    private void Update()
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
                closeLevelSelect();
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
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1));
    }

    private void closeLevelSelect()
    {
        play.SetActive(true);
        ninjaMainMenu.SetActive(true);
        levelSelection.SetActive(false);
    }

    public void SelectLevel()
    {
        play.SetActive(false);
        ninjaMainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }
}
