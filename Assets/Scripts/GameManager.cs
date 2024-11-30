using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject player;
    public GameObject particles;

    public mischiefSystem mischiefSyst;
    public objectiveSystem objectiveSyst;

    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }
    }

}
