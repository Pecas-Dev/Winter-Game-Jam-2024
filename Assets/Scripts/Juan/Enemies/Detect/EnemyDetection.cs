using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyDetection : MonoBehaviour
{
    [Header("Field of View Settings")]
    [SerializeField] float viewRadius = 5f;
    [SerializeField][Range(0, 360)] float viewAngle = 90f;
    [SerializeField] LayerMask targetMask;

    EnemyMovement enemyMovement;
    Transform player;

    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        FieldOfViewCheck();
    }

    void FieldOfViewCheck()
    {
        if (player == null)
        {
            return;
        }

        Vector2 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= viewRadius)
        {
            float angleBetweenEnemyAndPlayer = Vector2.Angle(enemyMovement.FacingDirection, directionToPlayer);

            if (angleBetweenEnemyAndPlayer <= viewAngle / 2)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, distanceToPlayer, targetMask);

                if (hit)
                {
                    Debug.Log("Player detected!");
                    GameEvents.PlayerCaught(); // Invoke the event
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        if (enemyMovement == null)
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 facingDirection3D = new Vector3(enemyMovement.FacingDirection.x, enemyMovement.FacingDirection.y, 0);

        float halfViewAngle = viewAngle / 2;

        Vector3 viewAngleA = Quaternion.Euler(0, 0, -halfViewAngle) * facingDirection3D;
        Vector3 viewAngleB = Quaternion.Euler(0, 0, halfViewAngle) * facingDirection3D;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
    }
}
