using UnityEngine;
using UnityEngine.UI;

public class MischiefSlider : MonoBehaviour
{
    [Header("Image References (Emojis)")]
    [SerializeField] Image demonSantaImage;
    [SerializeField] Image angelSantaImage;


    [Header("Image References (Slider)")]
    [SerializeField] Image mischiefImage; 


    [Header("Slider Settings")]
    [SerializeField] float fillDuration = 10f;
    [SerializeField] float alphaDemonAngelGrace = 0.15f; 


    float currentFillAmount = 0f;
    float elapsedTime = 0f;


    void Start()
    {
        mischiefImage.fillAmount = 0f;

        SetImageAlpha(demonSantaImage, 0f);
        SetImageAlpha(angelSantaImage, 1f);
    }

    void Update()
    {
        if (elapsedTime < fillDuration)
        {
            UpdateMischiefSlider();
        }
    }

    void UpdateMischiefSlider()
    {
        elapsedTime += Time.deltaTime;
        currentFillAmount = Mathf.Clamp01(elapsedTime / fillDuration);

        mischiefImage.fillAmount = currentFillAmount;

        SetImageAlpha(demonSantaImage, currentFillAmount - alphaDemonAngelGrace);
        SetImageAlpha(angelSantaImage, 1f - (currentFillAmount + alphaDemonAngelGrace));

        Color startColor = HexToColor("75FBFF");
        Color endColor = HexToColor("9400FF");

        mischiefImage.color = Color.Lerp(startColor, endColor, currentFillAmount);
    }

    void SetImageAlpha(Image img, float alpha)
    {
        Color color = img.color;

        color.a = alpha;
        img.color = color;
    }

    Color HexToColor(string hex)
    {
        Color color;

        if (ColorUtility.TryParseHtmlString("#" + hex, out color))
        {
            return color;
        }

        else
        {
            Debug.LogWarning("Invalid hex color: " + hex);

            return Color.white;
        }
    }
}
