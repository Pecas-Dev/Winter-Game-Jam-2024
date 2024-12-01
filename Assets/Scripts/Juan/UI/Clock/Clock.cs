using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [Header("Image References")]
    [SerializeField] Image clockImage;

    [Header("Time Manager Reference")]
    [SerializeField] TimeManager timeManager;

    float startingTime;

    void Awake()
    {
        if (timeManager != null)
        {
            startingTime = timeManager.GetStartingTime();
        }
        else
        {
            Debug.LogWarning("TimeManager not found in the scene!");
        }
    }

    void Update()
    {
        ClockTimePass();
    }

    void ClockTimePass()
    {
        if (timeManager != null)
        {
            float currentTime = timeManager.GetCurrentTime();

            float fillAmount = currentTime / startingTime;

            clockImage.fillAmount = fillAmount;
        }
    }
}
