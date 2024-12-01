using TMPro;
using UnityEngine;

public class TimeDisplay : MonoBehaviour
{
    [Header("Text Mesh Pro Time Text Reference")]
    [SerializeField] TextMeshProUGUI timeText;

    [Header("Time Manager Reference")]
    [SerializeField] TimeManager timeManager;

    void Update()
    {
        DisplayTime();
    }

    void DisplayTime()
    {
        if (timeManager != null)
        {
            float currentTime = timeManager.GetCurrentTime();

            int minutes = Mathf.FloorToInt(currentTime / 60f);
            int seconds = Mathf.FloorToInt(currentTime % 60f);

            string timeString = string.Format("{0}:{1:00}", minutes, seconds);

            timeText.text = timeString;
        }
    }
}
