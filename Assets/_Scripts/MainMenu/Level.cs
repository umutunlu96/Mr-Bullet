using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Level : MonoBehaviour
{
    private Button levelBtn;
    public int levelReq;
    private Text levelNumber;

    private void Awake()
    {
        levelReq = transform.GetSiblingIndex() + 1;
        levelNumber = transform.GetChild(0).GetComponent<Text>();
    }

    void Start()
    {
        levelNumber.text = levelReq.ToString();

        levelBtn = GetComponent<Button>();

        if (PlayerPrefs.GetInt("Level", 1) >= levelReq)
            levelBtn.onClick.AddListener(() => LoadLevel());
        else
            GetComponent<CanvasGroup>().alpha = .5f;
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene("Level" + levelReq.ToString());
    }

    void Update()
    {
        
    }
}
