using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] string mainMenuScene = "MainMenu";
    [SerializeField] string gameScene = "Game";
    [SerializeField] string creditsScene = "Credits";
    [SerializeField] string winScene = "Win Scene";
    [SerializeField] string loseScene = "Lose Scene";

    [Header("URL")]
    [Tooltip("URL For Exiting Game in Web Version")]
    [SerializeField] string webURL = "";


    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void LoadCreditsScene()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene(winScene);
    }

    public void LoadLoseScene()
    {
        SceneManager.LoadScene(loseScene);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void ExitGame()
    {
        Debug.Log("Game Closed!");

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Application.OpenURL(webURL);
            Application.Quit();
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            Application.Quit();
        }
        else
        {
            Application.Quit();
        }
    }
}
