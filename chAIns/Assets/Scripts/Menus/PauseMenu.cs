using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject canvas;

    private bool pausePressed;
    private bool isPaused;

    // Update is called once per frame
    void Update()
    {
        pausePressed = Input.GetKeyDown(KeyCode.Escape);
        
        if(pausePressed && !isPaused)
        {
            Pause();
        }

        else if (pausePressed && isPaused)
        {
            Unpause();
        }

    }

    public void Pause()
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

    }

    public void Unpause()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
