using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public GameObject player;
    public GameObject particles;


    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(this); }
        else { instance = this; }
    }

}
