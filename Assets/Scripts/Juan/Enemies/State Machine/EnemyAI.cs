using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyAI : MonoBehaviour
{
    EnemyMovement enemyMovement;
    EnemyPatrol enemyPatrol;

    enum State { Patrolling, Alerted, Investigating }
    State currentState = State.Patrolling;


    Vector2 lastHeardNoisePosition;


    [Header("Hearing Settings")]
    [SerializeField] float hearingRadius = 10f;
    [SerializeField] float alertDuration = 2f;
    [SerializeField] float investigateDuration = 3f;


    float alertTimer;
    float investigateTimer;


    void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    void OnEnable()
    {
        NoiseManager.OnNoiseMade += OnNoiseMade;
    }

    void OnDisable()
    {
        NoiseManager.OnNoiseMade -= OnNoiseMade;
    }

    void OnNoiseMade(Vector2 noisePosition)
    {
        float distanceToNoise = Vector2.Distance(transform.position, noisePosition);

        if (distanceToNoise <= hearingRadius)
        {
            currentState = State.Alerted;
            lastHeardNoisePosition = noisePosition;
            alertTimer = alertDuration;

            enemyPatrol.enabled = false;
            enemyMovement.SetMovementInput(Vector2.zero);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                break;
            case State.Alerted:
                HandleAlertedState();
                break;
            case State.Investigating:
                HandleInvestigatingState();
                break;
            default:
                break;
        }
    }

    void HandleAlertedState()
    {
        Vector2 directionToNoise = (lastHeardNoisePosition - (Vector2)transform.position).normalized;
        enemyMovement.SetFacingDirection(directionToNoise);

        alertTimer -= Time.deltaTime;
        if (alertTimer <= 0f)
        {
            currentState = State.Investigating;
            enemyMovement.SetMovementInput(directionToNoise);
            investigateTimer = investigateDuration;
        }
    }

    void HandleInvestigatingState()
    {
        float distanceToNoise = Vector2.Distance(transform.position, lastHeardNoisePosition);

        if (distanceToNoise < 0.5f)
        {
            enemyMovement.SetMovementInput(Vector2.zero);
            investigateTimer -= Time.deltaTime;

            if (investigateTimer <= 0f)
            {
                currentState = State.Patrolling;
                enemyPatrol.enabled = true;
            }
        }
        else
        {
            Vector2 directionToNoise = (lastHeardNoisePosition - (Vector2)transform.position).normalized;
            enemyMovement.SetMovementInput(directionToNoise);
        }
    }
}
