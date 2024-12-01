using System;
using UnityEngine;

public class interactableObject : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] GameObject interactionText;
    [SerializeField] interactionZone interactionZone;
    [SerializeField] Item itemChar;

    bool interactable;
    bool inOriginalState;

    [Header("Test Variables")]
    public string state;

    GameInput gameInput;

    void Awake()
    {
        interactionText.SetActive(false);
        inOriginalState = true;

        gameInput = FindObjectOfType<GameInput>();

        if (gameInput != null)
        {
            gameInput.OnInteractPerformed += OnInteractAction;
        }
        else
        {
            Debug.LogWarning("GameInput not found in the scene.");
        }
    }

    void OnDestroy()
    {
        if (gameInput != null)
        {
            gameInput.OnInteractPerformed -= OnInteractAction;
        }
    }

    void Update()
    {
        interactable = interactionZone.isInteractedWith;

        if (interactable)
        {
            state = "interacting";
            if (inOriginalState)
            {
                interactionText.SetActive(true);
            }
        }
        else
        {
            interactionText.SetActive(false);
            state = "not interacting";
        }
    }

    void OnInteractAction()
    {
        if (interactable && inOriginalState)
        {
            inOriginalState = false;
            InteractionChange();
        }
    }

    void InteractionChange()
    {
        itemChar.interaction();
        GameManager.instance.player.GetComponent<playerInteraction>().CauseParticles();

        NoiseManager.MakeNoise(transform.position);

        PlayerAnimator playerAnimator = FindAnyObjectByType<PlayerAnimator>();

        if (playerAnimator != null)
        {
            playerAnimator.SetIsStealing(true);
        }
        else
        {
            Debug.LogWarning("PlayerAnimator not found on the player.");
        }

        if (itemChar.disappear)
        {
            gameObject.SetActive(false);
        }
        if (itemChar.changeState)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = itemChar.changed;
        }
    }

    /*void InteractionChange()
    {
        string tag = gameObject.tag;
        GameManager.instance.player.GetComponent<playerInteraction>().CauseParticles();


        if (tag == "InteractableObject")
        {
            GameManager.instance.mischiefSyst.addPoints(100);
            gameObject.SetActive(false);
            return;
        }

        if (tag == "Vase")
        {
            GameManager.instance.objectiveSyst.DestroyVases();
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into broken vase
            return;
        }
        if (tag == "Carpet")
        {
            GameManager.instance.objectiveSyst.StainCarpets();
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into stained carpet
            return;
        }
        if (tag == "Present")
        {
            GameManager.instance.objectiveSyst.StealPresents(); // steal = disable gameobject
            gameObject.SetActive(false);
            return;
        }
        if (tag == "ChristmasTree")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Set Christmas Tree on fire");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into burnt tree
            return;
        }
        if (tag == "Cat")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Steal a cat");
            gameObject.SetActive(false); // steal = disable gameobject
            return;
        }
        if (tag == "Couch")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Rip the couch");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into a ripped couch
            return;
        }
        if (tag == "MilkAndCookies")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Spill milk and eat the cookies");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // unto empty milk cup and cookie crumbs
            return;
        }
        if (tag == "Fridge")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Unplug the fridge");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into open fridge with spoiled food
            return;
        }
        if (tag == "Toilet")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Clog the toilet");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into clogged toilet
            return;
        }
        if (tag == "Glogg")
        {
            GameManager.instance.objectiveSyst.OtherConditionMet("Chug all the Glögg");
            gameObject.GetComponent<SpriteRenderer>().sprite = changed; // into empty bottle
            return;
        }
     
    }*/
}
