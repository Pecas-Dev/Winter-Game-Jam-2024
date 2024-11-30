using UnityEngine;

public class interactableObject : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private GameObject interactionText;
    [SerializeField] private interactionZone interactionZone;
    [SerializeField] private Sprite original;
    [SerializeField] private Sprite changed;

    private bool interactable;
    private bool inOriginalState;

    [Header("Test Variables")]
    public string state;

    void Start()
    {
        interactionText.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = original;
        inOriginalState = true;
    }

    // Update is called once per frame
    void Update()
    {
        interactable = interactionZone.isInteractedWith;

        if (interactable)
        {
            state = "interacting";
            if (inOriginalState == true)
            {
                interactionText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    inOriginalState = false;
                    gameObject.GetComponent<SpriteRenderer>().sprite = changed;
                }
            }
        } else
        {
            interactionText.SetActive(false);
            state = "not interacting";
        }
    }
}
