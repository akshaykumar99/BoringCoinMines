﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Text highscoreText;
    // Start is called before the first frame update
    void Start() 
    {
        highscoreText.text = "Highscore : " + ((int)PlayerPrefs.GetFloat("Highscore")).ToString() ;
    }

    // Update is called once per frame
    public void ToGame()
    {
        SceneManager.LoadScene("Game");
    }
}
