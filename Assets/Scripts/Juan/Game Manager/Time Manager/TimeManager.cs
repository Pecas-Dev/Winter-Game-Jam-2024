using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] float startingTime = 60f;

    float currentTime;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;

            OnTimerEnd();
        }
    }

    void OnTimerEnd()
    {
        Debug.Log("Timer has ended!");
        GameEvents.TimeRunOut();
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public float GetStartingTime()
    {
        return startingTime;
    }
}
