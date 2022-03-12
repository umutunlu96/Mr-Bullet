using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject levelSelection;

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
