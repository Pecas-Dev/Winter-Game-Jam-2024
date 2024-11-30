using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] float minTimeToWait = 2.5f;
    [SerializeField] float maxTimeToWait = 4.0f;


    public float waitTimeAtNode = 1f;
    public Transform[] moveNodes;


    EnemyMovement enemyMovement;


    int currentNodeIndex;

    float waitTime;


    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();

        waitTimeAtNode = Random.Range(minTimeToWait, maxTimeToWait);
    }

    void Start()
    {
        if (moveNodes.Length == 0)
        {
            Debug.LogWarning("No move nodes assigned for enemy patrol.");

            enabled = false;

            return;
        }

        // Start at a random node
        currentNodeIndex = Random.Range(0, moveNodes.Length);
        transform.position = moveNodes[currentNodeIndex].position;

        // Prepare to move to the next node
        IncrementNodeIndex();

        waitTime = waitTimeAtNode;
    }

    void Update()
    {
        Patrol();
    }

    void Patrol()
    {
        if (waitTime > 0)
        {
            // Waiting at the node
            waitTime -= Time.deltaTime;
            enemyMovement.SetMovementInput(Vector2.zero); 

            // Set facing direction towards the next node
            Vector2 directionToNode = (moveNodes[currentNodeIndex].position - transform.position).normalized;

            enemyMovement.SetFacingDirection(directionToNode);
        }
        else
        {
            // Calculate direction to the next node
            Vector2 directionToNode = (moveNodes[currentNodeIndex].position - transform.position).normalized;

            enemyMovement.SetMovementInput(directionToNode);

            // Check if the enemy has reached the node
            float distanceToTarget = Vector2.Distance(transform.position, moveNodes[currentNodeIndex].position);

            if (distanceToTarget < 0.2f)
            {
                // Reached the node
                enemyMovement.SetMovementInput(Vector2.zero); 
                waitTimeAtNode = Random.Range(minTimeToWait, maxTimeToWait);
                waitTime = waitTimeAtNode; // Reset wait time

                IncrementNodeIndex(); // Move to the next node
            }
        }
    }

    void IncrementNodeIndex()
    {
        currentNodeIndex = (currentNodeIndex + 1) % moveNodes.Length;
    }
}
