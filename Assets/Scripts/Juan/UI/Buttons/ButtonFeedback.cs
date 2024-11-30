using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonFeedback : MonoBehaviour
{
    [Header("Scaling Settings")]
    [SerializeField] Vector3 normalScale = Vector3.one;
    [SerializeField] Vector3 hoverScale = Vector3.one * 1.1f;
    [SerializeField] Vector3 clickScale = Vector3.one * 0.9f;
    [SerializeField] float scaleDuration = 0.1f;


    bool isPointerOver = false;


    Coroutine scaleCoroutine;


    void Start()
    {
        transform.localScale = normalScale;
    }

    public void OnPointerEnter()
    {
        isPointerOver = true;
        StartScaling(hoverScale);
    }

    public void OnPointerExit()
    {
        isPointerOver = false;
        StartScaling(normalScale);
    }

    public void OnPointerClick()
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        StartCoroutine(ClickFeedback());
    }

    void StartScaling(Vector3 targetScale)
    {
        if (scaleCoroutine != null)
        {
            StopCoroutine(scaleCoroutine);
        }

        scaleCoroutine = StartCoroutine(ScaleRoutine(targetScale));
    }

    IEnumerator ScaleRoutine(Vector3 targetScale)
    {
        Vector3 initialScale = transform.localScale;
        float time = 0f;

        while (time < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, targetScale, time / scaleDuration);
            time += Time.unscaledDeltaTime; // Use unscaledDeltaTime for UI elements
            yield return null;
        }

        transform.localScale = targetScale;
        scaleCoroutine = null;
    }

    IEnumerator ClickFeedback()
    {
        Vector3 initialScale = transform.localScale;
        float time = 0f;

        while (time < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(initialScale, clickScale, time / scaleDuration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = clickScale;

        Vector3 targetScale = isPointerOver ? hoverScale : normalScale;
        time = 0f;
        while (time < scaleDuration)
        {
            transform.localScale = Vector3.Lerp(clickScale, targetScale, time / scaleDuration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        scaleCoroutine = null;
    }
}
