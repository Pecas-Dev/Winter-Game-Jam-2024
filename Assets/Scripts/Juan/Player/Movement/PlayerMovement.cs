using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 30f;
    [SerializeField] float deceleration = 20f;

    [Header("References")]
    [SerializeField] GameInput gameInput;

    [Header("Isometric Settings")]
    [SerializeField] float isometricAngle = 45f;

    bool isWalking = false;
    bool isMovementEnabled = true;

    Rigidbody2D playerRigidbody;

    Vector2 movementInput;
    public Vector2 currentVelocity;
    public Vector2 MovementInput => movementInput;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gameInput = FindFirstObjectByType<GameInput>();

        GameEvents.OnPlayerCaught += DisableMovement;
        GameEvents.OnTimeRunOut += DisableMovement;
    }

    void OnDestroy()
    {
        GameEvents.OnPlayerCaught -= DisableMovement;
        GameEvents.OnTimeRunOut -= DisableMovement;
    }

    void Update()
    {
        if (isMovementEnabled)
        {
            movementInput = gameInput.MovementInput;
            movementInput = AdjustInputForIsometric(movementInput);
        }
        else
        {
            movementInput = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        Vector2 targetVelocity = movementInput.normalized * moveSpeed;
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

        playerRigidbody.linearVelocity = currentVelocity;

        isWalking = currentVelocity != Vector2.zero;
    }

    void DisableMovement()
    {
        isMovementEnabled = false;
        currentVelocity = Vector2.zero;
        playerRigidbody.linearVelocity = Vector2.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    public Vector2 GetMovementInput()
    {
        return movementInput;
    }

    Vector2 AdjustInputForIsometric(Vector2 input)
    {
        float angle = -isometricAngle * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float x = input.x * cos - input.y * sin;
        float y = input.x * sin + input.y * cos;

        return new Vector2(x, y);
    }
}
