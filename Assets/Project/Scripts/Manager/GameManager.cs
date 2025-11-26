using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public StarterAssetsInputs input;

    private void Awake()
    {
        // Ensure we respond to scene loads
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        GameSaver.Instance.SaveGame();

        // Try to resolve input if not set in inspector
        if (input == null)
        {
            input = FindObjectOfType<StarterAssetsInputs>();
        }

        // Ensure cursor state for the initial scene
        ApplyCursorStateForScene(SceneManager.GetActiveScene().name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Resolve input again because scene objects may have been recreated
        if (input == null)
        {
            input = FindObjectOfType<StarterAssetsInputs>();
        }

        ApplyCursorStateForScene(scene.name);
    }

    private void ApplyCursorStateForScene(string sceneName)
    {
        if (!sceneName.Equals("Menu"))
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (input != null) input.cursorLocked = true;
        }
        else
        {
            // In Menu we want the cursor visible / unlocked
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            if (input != null) input.cursorLocked = false;
        }
    }

    private void Update()
    {
        if (!SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                OnPauseGame();
            }
        }
    }

    public void OnPauseGame()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnResumeGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (input == null) input = FindObjectOfType<StarterAssetsInputs>();
        if (input != null) input.cursorLocked = true;
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        GameSaver.Instance.SaveGame();
        SceneManager.LoadScene("Menu");
    }

    public void OnRestartGame()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameSaver.Instance.LoadGame();
        if (input == null) input = FindObjectOfType<StarterAssetsInputs>();
        if (input != null) input.cursorLocked = true;
    }
}
