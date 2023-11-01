using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Static Variables

    [Space] [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [Space] [Header("Detection Settings")]
    [SerializeField] private float ray;
    [SerializeField] private LayerMask layer;
    private Rigidbody2D body;
    private Animator animator;
    private Collider2D _collider;

    #endregion

    #region Dynamic Variables

    private float horizontal
    {
        get
        {
            float x = Input.GetAxisRaw("Horizontal");

            // flip when going left
            transform.localScale = new Vector2(x < 0 ? -1 : 1, 1);

            return x;
        }
    }

    private bool isRunning
    {
        get
        {
            bool running = Mathf.Abs(horizontal) > 0;
            animator.SetBool("IsRunning", running);
            return running;
        }
    }

    private bool isGrounded
    {
        get
        {
            animator.SetBool("IsJumping", body.velocity.y > 0);
            animator.SetBool("IsFalling", body.velocity.y < 0);

            Bounds bounds = _collider.bounds;
            Vector2 center = bounds.center;
            float detectionDistance = bounds.extents.y + ray;

            Vector2 left = new Vector2(center.x - bounds.extents.x, center.y);
            Vector2 right = new Vector2(center.x + bounds.extents.x, center.y);

            // detect ground layer
            RaycastHit2D leftHit = Physics2D.Raycast(left, Vector2.down, detectionDistance, layer);
            RaycastHit2D rightHit = Physics2D.Raycast(right, Vector2.down, detectionDistance, layer);

            Debug.DrawRay(left, Vector2.down * detectionDistance, leftHit.collider != null ? Color.green : Color.red);
            Debug.DrawRay(right, Vector2.down * detectionDistance, rightHit.collider != null ? Color.green : Color.red);

            bool grounded = leftHit.collider != null || rightHit.collider != null;

            animator.SetBool("IsGrounded", grounded);

            return grounded;
        }
    }

    #endregion


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Run();
        Jump();
    }

    private void Run()
    {
        if (!isRunning) return;
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
    }

    private void Jump()
    {
        if (!isGrounded || !Input.GetKeyDown(KeyCode.Space)) return;
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
