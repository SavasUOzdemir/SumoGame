using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] GameObject pauseIcon;
    [SerializeField] GameObject resumeIcon;
    [SerializeField] GameObject gameOverText;
    [SerializeField] timerScript _timerScript;
    //check if esc is hit, then pause or unpause.
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                ResumeGame();            
            else           
                PauseGame();            
        }
        //game end condition. 
        int playerCount = GameObject.FindGameObjectsWithTag("Player").Length;

        if (playerCount == 1 || _timerScript.timeLeft < 0.1f)
        {
            PauseGame();
            StartCoroutine(nameof(GameOverStuff));
        }
    }
    //simple pause and resume functions. icon gameobjects are also buttons btw, clickable and hopefully touchable.
    public void PauseGame()
    {
        pauseIcon.SetActive(false);
        resumeIcon.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseIcon.SetActive(true);
        resumeIcon.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }
    //loop level
    IEnumerator GameOverStuff()
    {
        gameOverText.SetActive(true);
        yield return new WaitForSecondsRealtime(5f);
        gameOverText.SetActive(false);
        SceneManager.LoadScene(0);
        ResumeGame();
    }
}
