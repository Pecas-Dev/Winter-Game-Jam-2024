using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class roomCollision : MonoBehaviour
{
    [SerializeField] private GameObject roomGO;
    public bool playerIsIn = false;
    private bool isFadingIn = false;
    private bool isFadingOut = false;
    private float fadeRate;

    private void Start()
    {
        fadeRate = 5f;
    }

    public void OnRoomExit()
    {
        playerIsIn = false;

        isFadingIn = false;
        isFadingOut = true;

        //if (isFadingOut == false) { StartCoroutine(FadeOut(0.1f)); }
        //if (isFadingOut == true) { StopCoroutine(FadeOut(0.1f)); StartCoroutine(FadeOut(0.1f)); }
        // if is fading out = true, 
    }

    public void OnRoomEnter()
    {
        playerIsIn = true;

        isFadingIn = true;
        isFadingOut = false;
        //if (isFadingIn == false) { StartCoroutine(FadeIn(0.1f)); }
        //if (isFadingIn == true) { StopCoroutine(FadeIn(0.1f)); StartCoroutine(FadeIn(0.1f)); }
        //StartCoroutine(FadeIn(0.1f));
    }


    private void Update()
    {
        if (isFadingIn)
        {
            Color tmp = roomGO.GetComponent<SpriteRenderer>().color;
            tmp.a += fadeRate * Time.deltaTime;
            tmp.a = Mathf.Clamp01(tmp.a);

            for (int i = 0; i < roomGO.transform.childCount; i++)
            {
                if (roomGO.transform.GetChild(i).gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer rend))
                {
                    rend.color = tmp;
                }
                if (roomGO.transform.GetChild(i).gameObject.TryGetComponent<Light>(out Light light))
                {
                    light.intensity = tmp.a;
                }
            }

            roomGO.GetComponent<SpriteRenderer>().color = tmp;
        }

        if (isFadingOut)
        {
            Color tmp = roomGO.GetComponent<SpriteRenderer>().color;
            tmp.a -= fadeRate * Time.deltaTime;
            tmp.a = Mathf.Clamp01(tmp.a);

            for (int i = 0; i < roomGO.transform.childCount; i++)
            {
                if (roomGO.transform.GetChild(i).gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer rend))
                {
                    rend.color = tmp;
                }
                if (roomGO.transform.GetChild(i).gameObject.TryGetComponent<Light>(out Light light))
                {
                    light.intensity = tmp.a;
                }
            }

            roomGO.GetComponent<SpriteRenderer>().color = tmp;
        }

        if (roomGO.GetComponent<SpriteRenderer>().color.a == 0) { isFadingOut = false; }
        if (roomGO.GetComponent<SpriteRenderer>().color.a == 1) { isFadingIn = false; }
    }

    IEnumerator FadeIn(float fadeRate)
    {
        isFadingIn = true;
        Debug.Log(gameObject.name + " fade in");

        Color tmp = roomGO.GetComponent<SpriteRenderer>().color;
        while (tmp.a < 1)
        {
            tmp.a += fadeRate;
            roomGO.GetComponent<SpriteRenderer>().color = tmp;

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.05f);
        Debug.Log("fade in finished");
        isFadingIn = false;

    }

    IEnumerator FadeOut(float fadeRate)
    {
        isFadingOut = true;
        Debug.Log(gameObject.name + " fade out");
        Color tmp = roomGO.GetComponent<SpriteRenderer>().color;
        while (tmp.a > 0)
        {
            tmp.a -= fadeRate;
            roomGO.GetComponent<SpriteRenderer>().color = tmp;

            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.05f);
        Debug.Log("fade out finished");
        isFadingOut = false;

    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

}
