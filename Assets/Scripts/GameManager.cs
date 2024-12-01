using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject player;
    public GameObject particles;

    public mischiefSystem mischiefSyst;
    public objectiveSystem objectiveSyst;

    public GameSceneManager gameSceneManager;

    void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }

        gameSceneManager = FindAnyObjectByType<GameSceneManager>();
    }

    public void GameWon()
    {
        Debug.Log("Game won!");
        GameEvents.GameWon();
    }
}
