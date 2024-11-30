using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    const string F_ENEMY_SPEED = "enemySpeed";
    const string F_ENEMY_HORIZONTAL = "enemyHorizontal";
    const string F_ENEMY_VERTICAL = "enemyVertical";

    [Header("References")]
    [SerializeField] EnemyMovement enemyMovement;
    [SerializeField] Light2D enemyLight;

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        UpdateAnimator();
        UpdateVisualRotation();
    }

    void UpdateAnimator()
    {
        Vector2 moveInput = enemyMovement.MovementInput;
        float speed = moveInput.magnitude;

        Vector2 inputDirection = (speed > 0.01f) ? moveInput.normalized : enemyMovement.FacingDirection.normalized;

        animator.SetFloat(F_ENEMY_HORIZONTAL, inputDirection.x);
        animator.SetFloat(F_ENEMY_VERTICAL, inputDirection.y);
        animator.SetFloat(F_ENEMY_SPEED, speed);
    }

    void UpdateVisualRotation()
    {
        Vector2 facingDir = enemyMovement.FacingDirection.normalized;

        float angle = Mathf.Atan2(facingDir.y, facingDir.x) * Mathf.Rad2Deg - 90.0f;

        if (enemyLight != null)
        {
            enemyLight.transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
