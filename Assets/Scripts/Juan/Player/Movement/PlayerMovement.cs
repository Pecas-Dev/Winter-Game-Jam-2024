using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float acceleration = 20f;
    [SerializeField] float deceleration = 10f;

    [Header("References")]
    [SerializeField] GameInput gameInput;


    bool isWalking = false;


    Rigidbody2D playerRigidbody;


    Vector2 movementInput;
    Vector2 currentVelocity;

    public Vector2 MovementInput => movementInput;

    void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        gameInput = FindFirstObjectByType<GameInput>();
    }

    void Update()
    {
        movementInput = gameInput.MovementInput;
        movementInput = AdjustInputForIsometric(movementInput);
    }

    void FixedUpdate()
    {
        UpdateMovement();
    }

    void UpdateMovement()
    {
        Vector2 targetVelocity = movementInput.normalized * moveSpeed;

        float speedDifference = targetVelocity.magnitude - currentVelocity.magnitude;
        float accelerationRate;

        if (Mathf.Abs(speedDifference) > 0.01f)
        {
            accelerationRate = (speedDifference > 0) ? acceleration : deceleration;
        }
        else
        {
            accelerationRate = deceleration;
        }

        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, accelerationRate * Time.fixedDeltaTime);

        if (currentVelocity.magnitude < 0.01f)
        {
            currentVelocity = Vector2.zero;
        }

        playerRigidbody.linearVelocity = currentVelocity;

        isWalking = currentVelocity != Vector2.zero;
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
        float angle = -45f * Mathf.Deg2Rad;
        float sin = Mathf.Sin(angle);
        float cos = Mathf.Cos(angle);

        float x = input.x * cos - input.y * sin;
        float y = input.x * sin + input.y * cos;

        return new Vector2(x, y);
    }
}
