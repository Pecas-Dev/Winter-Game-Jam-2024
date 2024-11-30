using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInStartButtonFunction : MonoBehaviour
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
            startColor.a = 1f; 
            fadeImage.color = startColor;
            fadeImage.gameObject.SetActive(false); 
        }
        else
        {
            Debug.LogWarning("Fade image is not assigned or missing Image component!");
        }
    }

    public void OnStartButtonClicked()
    {
        if (fadeImage != null)
        {
            fadeImage.gameObject.SetActive(true);
            StartCoroutine(FadeInCoroutine());
        }
    }

    IEnumerator FadeInCoroutine()
    {
        Color startColor = fadeImage.color;
        startColor.a = 0f;

        Color endColor = fadeImage.color;
        endColor.a = 1f; 

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = Color.Lerp(startColor, endColor, alpha);
            yield return null;
        }

        fadeImage.color = endColor;

        gameSceneManager.LoadGameScene();
    }
}
