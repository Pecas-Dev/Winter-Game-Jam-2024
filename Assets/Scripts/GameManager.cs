using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject player;
    public GameObject particles;

    public mischiefSystem mischiefSyst;
    public objectiveSystem objectiveSyst;

    public GameSceneManager gameSceneManager;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }


    }

    public void GameWon()
    {
        Debug.Log("oh yeah game won");
        gameSceneManager.LoadCreditsScene();
    }

}
