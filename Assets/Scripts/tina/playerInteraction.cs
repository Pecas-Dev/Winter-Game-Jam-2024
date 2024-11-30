using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    private bool causedChange = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionZone"))
        {
            collision.GetComponent<interactionZone>().isInteractedWith = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractionZone"))
        {
            collision.GetComponent<interactionZone>().isInteractedWith = false;
        }
    }

    public void CauseParticles()
    {
        GameObject particles = Instantiate(GameManager.instance.particles);
        particles.transform.position = gameObject.transform.position;
    }

    // if deciding to interact, teleport the particles to the player
}
