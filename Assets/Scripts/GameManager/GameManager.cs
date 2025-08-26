using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        
    }
    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
    public void OnPauseGame()
    {
        Time.timeScale = 0;
    }
    public void OnResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void BackToMenu()
    {
        Time.timeScale = 1;
        GameSaver.Instance.SaveGame();
        SceneManager.LoadScene("Menu");
    }
}
