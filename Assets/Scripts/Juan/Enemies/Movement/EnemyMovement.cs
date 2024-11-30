using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float acceleration = 20f;
    public float deceleration = 10f;


    Rigidbody2D enemyRigidbody;


    Vector2 movementInput;
    Vector2 currentVelocity;


    Vector2 facingDirection = Vector2.right; 

    public Vector2 MovementInput => movementInput;
    public Vector2 FacingDirection => facingDirection;

    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    public void SetMovementInput(Vector2 input)
    {
        movementInput = input.normalized;

        if (movementInput.magnitude > 0.01f)
        {
            facingDirection = movementInput;
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
}
