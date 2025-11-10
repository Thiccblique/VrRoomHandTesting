using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Public variables
    [Header("Timer")]
    public TextMeshProUGUI timerText;
    [Header("Player Controller")]
    public PlayerController playerController;

    //Private variables
    private float timer;
    private bool stopTimer = false;

    // Update is called once per frame
    void Update()
    {
        Timer();
        Reset();
        GameOver();
    }

    private void Reset()
    {
        // Check if the "R" key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Get the currently active scene
            Scene currentScene = SceneManager.GetActiveScene();

            // Reload the current scene
            SceneManager.LoadScene(currentScene.name);
        }
    }

    private void Timer()
    {
        if (stopTimer == false)
        {
            // Increment the timer by the time passed since the last frame
            timer += Time.deltaTime;

        }

        // Update the TextMeshPro text with the formatted timer value
        if (timerText != null)
        {
            timerText.text = "Time: " + timer.ToString("F2") + " seconds";
        }
    }

    public void GameOver()
    {
        //Checks if player contoller is added in inspector
        if (playerController != null)
        {
            //Stops UI timer if gameOver is activated 
            if (playerController.gameOver == true)
            {
                stopTimer = true;
            }
        }
    }
}
