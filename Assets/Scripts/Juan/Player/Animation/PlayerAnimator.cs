using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    const string F_PLAYER_SPEED = "playerSpeed";
    const string F_PLAYER_HORIZONTAL = "playerHorizontal";
    const string F_PLAYER_VERTICAL = "playerVertical";

    [Header("References")]
    [SerializeField] PlayerMovement playerMovement;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();

        if (playerMovement == null)
        {
            playerMovement = GetComponent<PlayerMovement>();
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
    }
}
