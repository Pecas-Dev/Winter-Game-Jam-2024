using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    const string F_PLAYER_SPEED = "playerSpeed";
    const string F_PLAYER_HORIZONTAL = "playerHorizontal";
    const string F_PLAYER_VERTICAL = "playerVertical";

    [Header("References")]
    [SerializeField] PlayerMovement playerMovement;


    SpriteRenderer spriteRenderer;
    Animator animator;


    float lastHorizontalDirection = 1f;


    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (playerMovement == null)
        {
            playerMovement = GetComponentInParent<PlayerMovement>();
        }
    }

    void Update()
    {
        UpdateAnimator();
    }

    void UpdateAnimator()
    {
        Vector2 moveInput = playerMovement.GetMovementInput();

        float speed = moveInput.magnitude;

        Vector2 normalizedInput = speed > 0.01f ? moveInput.normalized : Vector2.zero;

        animator.SetFloat(F_PLAYER_HORIZONTAL, normalizedInput.x);
        animator.SetFloat(F_PLAYER_VERTICAL, normalizedInput.y);
        animator.SetFloat(F_PLAYER_SPEED, speed);

        if (speed > 0.01f && Mathf.Abs(normalizedInput.x) > 0.01f)
        {
            lastHorizontalDirection = Mathf.Sign(normalizedInput.x);
        }

        HandleSpriteFlip(speed);
    }

    void HandleSpriteFlip(float speed)
    {
        if (speed < 0.01f) 
        {
            spriteRenderer.flipX = lastHorizontalDirection < 0f;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
