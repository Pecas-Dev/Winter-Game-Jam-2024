using UnityEngine;

public class interactableObject : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] private GameObject interactionText;
    [SerializeField] private interactionZone interactionZone;
    [SerializeField] private Sprite changed;


    private bool interactable;
    private bool inOriginalState;

    [Header("Test Variables")]
    public string state;

    void Start()
    {
        interactionText.SetActive(false);
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
                    InteractionChange();
                }
            }
        } else
        {
            interactionText.SetActive(false);
            state = "not interacting";
        }
    }

    void InteractionChange()
    {
        string tag = gameObject.tag;

        if (tag == "Vase")
        {
            objectiveSystem.instance.DestroyVases();
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into broken vase
            return;
        }
        if (tag == "Carpet")
        {
            objectiveSystem.instance.StainCarpets();
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into stained carpet
            return;
        }
        if (tag == "Present")
        {
            objectiveSystem.instance.StealPresents(); // steal = disable gameobject
            gameObject.SetActive(false);
            return;
        }
        if (tag == "ChristmasTree")
        {
            objectiveSystem.instance.OtherConditionMet("Set Christmas Tree on fire");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into burnt tree
            return;
        }
        if (tag == "Cat")
        {
            objectiveSystem.instance.OtherConditionMet("Steal a cat");
            gameObject.SetActive(false); // steal = disable gameobject
            return;
        }
        if (tag == "Couch")
        {
            objectiveSystem.instance.OtherConditionMet("Rip the couch");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into a ripped couch
            return;
        }
        if (tag == "MilkAndCookies")
        {
            objectiveSystem.instance.OtherConditionMet("Spill milk and eat the cookies");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // unto empty milk cup and cookie crumbs
            return;
        }
        if (tag == "Fridge")
        {
            objectiveSystem.instance.OtherConditionMet("Unplug the fridge");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into open fridge with spoiled food
            return;
        }
        if (tag == "Toilet")
        {
            objectiveSystem.instance.OtherConditionMet("Clog the toilet");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into clogged toilet
            return;
        }
        if (tag == "Glogg")
        {
            objectiveSystem.instance.OtherConditionMet("Chug all the Glögg");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into empty bottle
            return;
        }
     
    }
}
