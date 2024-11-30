using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutExitButtonFunction : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] float fadeDuration = 1.0f;

    [Header("References")]
    [SerializeField] GameSceneManager gameSceneManager;


    Image fadeImage;


    void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    void Start()
    {
        if (fadeImage != null)
        {
            Color startColor = fadeImage.color;

            startColor.a = 0f;

            fadeImage.color = startColor;
            fadeImage.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Fade image is not assigned in the inspector!");
        }
    }

    public void OnExitButtonClicked()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);

            StartCoroutine(FadeOutCoroutine());
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        Color startColor = fadeImage.color;
        startColor.a = 0f;

        Color endColor = fadeImage.color;
        endColor.a = 1f;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // Use unscaled time for UI effects
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = Color.Lerp(startColor, endColor, alpha);
            yield return null;
        }

        fadeImage.color = endColor;

        gameSceneManager.ExitGame();
    }
}
