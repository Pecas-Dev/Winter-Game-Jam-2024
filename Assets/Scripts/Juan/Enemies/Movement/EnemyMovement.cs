using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float deceleration = 10f;

    Rigidbody2D enemyRigidbody;

    Vector2 movementInput;
    Vector2 currentVelocity;

    bool isMovementEnabled = true;

    Vector2 facingDirection = Vector2.right;

    public Vector2 MovementInput => movementInput;
    public Vector2 FacingDirection => facingDirection;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();

        GameEvents.OnPlayerCaught += DisableMovement;
        GameEvents.OnTimeRunOut += DisableMovement;
    }

    void OnDestroy()
    {
        GameEvents.OnPlayerCaught -= DisableMovement;
        GameEvents.OnTimeRunOut -= DisableMovement;
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    public void SetMovementInput(Vector2 input)
    {
        if (isMovementEnabled)
        {
            movementInput = input.normalized;

            if (movementInput.magnitude > 0.01f)
            {
                facingDirection = movementInput;
            }
        }
        else
        {
            movementInput = Vector2.zero;
        }
    }

    public void SetFacingDirection(Vector2 direction)
    {
        if (direction.magnitude > 0.01f)
        {
            facingDirection = direction.normalized;
        }
    }

    void UpdateMovement()
    {
        Vector2 targetVelocity = movementInput * moveSpeed;
        Vector2 velocityDifference = targetVelocity - currentVelocity;

        bool isChangingDirection = Vector2.Dot(currentVelocity, targetVelocity) < 0f;

        float accelerationRate;

        if (isChangingDirection)
        {
            accelerationRate = deceleration * 2f;
        }
        else
        {
            accelerationRate = (targetVelocity.magnitude > currentVelocity.magnitude) ? acceleration : deceleration;
        }

        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, accelerationRate * Time.fixedDeltaTime);

        if (currentVelocity.magnitude < 0.01f)
        {
            currentVelocity = Vector2.zero;
        }

        enemyRigidbody.linearVelocity = currentVelocity;
    }

    void DisableMovement()
    {
        isMovementEnabled = false;
        currentVelocity = Vector2.zero;
        enemyRigidbody.linearVelocity = Vector2.zero;
    }
}
