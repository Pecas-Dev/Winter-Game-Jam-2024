using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class GameWonFadeOut : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeDuration = 2f;
    [SerializeField] float afterFadeWait = 2f;

    [Header("Light Reference")]
    [SerializeField] Light2D globalLight;

    [Header("Scene Manager Reference")]
    [SerializeField] GameSceneManager gameSceneManager;

    void Awake()
    {
        if (fadeImage != null)
        {
            Color tempColor = fadeImage.color;
            tempColor.a = 0f;
            fadeImage.color = tempColor;
        }
        else
        {
            Debug.LogWarning("Fade Image not assigned in GameWonFadeOut.");
        }

        if (globalLight == null)
        {
            Debug.LogWarning("Global Light not assigned in GameWonFadeOut.");
        }

        GameEvents.OnGameWon += StartFadeOut;
    }

    void OnDestroy()
    {
        GameEvents.OnGameWon -= StartFadeOut;
    }

    void StartFadeOut()
    {
        StartCoroutine(FadeOutCoroutine());
    }

    IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;

        float initialIntensity = globalLight != null ? globalLight.intensity : 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float t = Mathf.Clamp01(elapsedTime / fadeDuration);

            color.a = t;
            fadeImage.color = color;

            if (globalLight != null)
            {
                globalLight.intensity = Mathf.Lerp(initialIntensity, 0f, t);
            }

            yield return null;
        }

        color.a = 1f;
        fadeImage.color = color;

        if (globalLight != null)
        {
            globalLight.intensity = 0f;
        }

        yield return new WaitForSeconds(afterFadeWait);

        OnFadeComplete();
    }

    void OnFadeComplete()
    {
        Debug.Log("Game won! Fade-out completed.");
        gameSceneManager.LoadWinScene();
    }
}
