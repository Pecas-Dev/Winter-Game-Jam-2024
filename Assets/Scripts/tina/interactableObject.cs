using UnityEngine;

public class interactableObject : MonoBehaviour
{
    [SerializeField] private GameObject interactionText;
    [SerializeField] private interactionZone interactionZone;
    private bool interactable;
    public string state;

    void Start()
    {
        interactionText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        interactable = interactionZone.isInteractedWith;

        if (interactable)
        {
            state = "interacting";

            if (Input.GetKey(KeyCode.E))
            {
                interactionText.SetActive(true);
                state = "text on";
            }
            else
            {
                interactionText.SetActive(false);

            }
        } else
        {
            interactionText.SetActive(false);
            state = "not interacting";
        }
    }
}
