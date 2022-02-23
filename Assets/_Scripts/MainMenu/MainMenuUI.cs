using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject play;
    public GameObject levelSelection;

    public void PlayGame()
    {
        play.SetActive(false);
        levelSelection.SetActive(true);
    }
}
