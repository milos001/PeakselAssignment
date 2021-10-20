using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private bool paused;
    
    private ScoreManager scoreManager;
    
    public GameObject pauseCanvas;

    private void Start()
    {
        scoreManager = GameObject.FindWithTag("Player").GetComponent<ScoreManager>();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        //pause if game goes to background
        if (!hasFocus && !scoreManager.gameEnded && !paused)
        {
            Pause();
            pauseCanvas.SetActive(true);
            paused = true;
        }
    }

    private void Update()
    {
        //pause if player hits back button during game
        if (Input.GetKey(KeyCode.Escape) && !scoreManager.gameEnded && !paused)
        {
            Pause();
            pauseCanvas.SetActive(true);
        }
        //pause if player hits back button during pause screen
        else if (Input.GetKey(KeyCode.Escape) && !scoreManager.gameEnded && paused)
        {
            UnPause();
            pauseCanvas.SetActive(false);
        }
    }

    public void Play()
    {
        //used by retry and play button on homescreen
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);
    }
    
    public void Pause()
    {
        //used by pause button/pause by setting timeScale to 0
        paused = true;
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        //used by continue button/unpause by setting timescale to 1(default)
        paused = false;
        Time.timeScale = 1;
    }

    public void Quit()
    {
        //used by quit button/quit game
        Application.Quit();
    }
}