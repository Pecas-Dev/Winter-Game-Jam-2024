using TMPro;
using UnityEngine;

public class mischiefSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;
    public int pointCount;

    void Start()
    {
        pointCount = 0;
    }

    public void addPoints(int amount)
    {
        pointCount += amount;
    }

    void Update()
    {
        pointsText.text = "Mischief points: " + pointCount.ToString();
    }
}
