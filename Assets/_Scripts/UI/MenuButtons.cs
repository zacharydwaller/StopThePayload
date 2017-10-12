using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuButtons : MonoBehaviour
{

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            GoToNextLevel();
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            int i = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(Mathf.Max(0, i - 1));
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        int i = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene((i + 1) % SceneManager.sceneCountInBuildSettings);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
