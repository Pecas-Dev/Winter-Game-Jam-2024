using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadTransition : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] float fadeDuration = 1.0f;

    [Header("References")]
    [SerializeField] Image fadeImage;


    void Start()
    {
        if (fadeImage != null)
        {
            Color startColor = fadeImage.color;
            startColor.a = 1f;
            fadeImage.color = startColor;

            fadeImage.gameObject.SetActive(true);

            StartCoroutine(FadeOutCoroutine());
        }
        else
        {
            Debug.LogWarning("Fade image is not assigned in the inspector!");
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        Color startColor = fadeImage.color;
        Color endColor = fadeImage.color;
        endColor.a = 0f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = Color.Lerp(startColor, endColor, alpha);
            yield return null;
        }

        fadeImage.color = endColor;
        fadeImage.gameObject.SetActive(false);
    }
}
