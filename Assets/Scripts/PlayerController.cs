using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Static Variables

    [Space] [Header("Movement Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float deathDelay;
    [SerializeField] private AudioSource jumpSound;

    [Space] [Header ("Dash Settings")]
    [SerializeField] private float dashIntensity;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    [Space] [Header("Detection Settings")]
    [SerializeField] private float ray;
    [SerializeField] private LayerMask layer;

    [Space] [Header("Color Indicator Settings")]
    [SerializeField] private GameObject ui;
    [SerializeField] private Material[] colors;

    private Rigidbody2D body;
    private Animator animator;
    private Collider2D _collider;

    private string[] deathBlocks;

    #endregion

    #region Dynamic Variables

    public static Transform spawnPoint;
    public static Renderer colorIndicator;
    private bool isDead;

    private bool isDashing;
    private bool canDash;

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
            Bounds bounds = _collider.bounds;
            Vector2 center = bounds.center;
            float detectionDistance = bounds.extents.y + ray;
            float offset = 0.35f;

            Vector2 left = new Vector2(center.x - bounds.extents.x + offset, center.y);
            Vector2 right = new Vector2(center.x + bounds.extents.x - offset, center.y);

            // detect ground layer
            RaycastHit2D leftHit = Physics2D.Raycast(left, Vector2.down, detectionDistance, layer);
            RaycastHit2D rightHit = Physics2D.Raycast(right, Vector2.down, detectionDistance, layer);

            Debug.DrawRay(left, Vector2.down * detectionDistance, leftHit.collider != null ? Color.green : Color.red);
            Debug.DrawRay(right, Vector2.down * detectionDistance, rightHit.collider != null ? Color.green : Color.red);

            bool grounded = leftHit.collider != null || rightHit.collider != null;

            animator.SetBool("IsGrounded", grounded);
            animator.SetBool("IsJumping", body.velocity.y > 0);
            animator.SetBool("IsFalling", body.velocity.y < 0);

            return grounded;
        }
    }

    #endregion

    private void Awake()
    {
        canDash = true;
        isDead = false;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        colorIndicator = ui.GetComponent<Renderer>();

        spawnPoint = transform;

        deathBlocks = new string[] { "Border", "Saw", "Spikes", "MainCamera"};
    }

    private void Update()
    {
        if (isDead) return;
        ColorChanger();

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash) StartCoroutine(Dash());
        if (isDashing) return;
        Run();
        Jump();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!deathBlocks.Contains(other.tag)) return;
        isDead = true;
        StartCoroutine(Death());
    }

    private void Run()
    {
        if (!isRunning) return;
        body.velocity = new Vector2(horizontal * speed, body.velocity.y);
    }

    private void Jump()
    {
        if (!isGrounded || !Input.GetKeyDown(KeyCode.Space)) return;
        jumpSound.Play();
        body.velocity = new Vector2(body.velocity.x, jumpForce);
    }

    private void ColorChanger()
    {
        colorIndicator.material = Input.GetKeyDown(KeyCode.Alpha1) ? colors[0] :
            Input.GetKeyDown(KeyCode.Alpha2) ? colors[1] :
            Input.GetKeyDown(KeyCode.Alpha3) ? colors[2] :
            colorIndicator.material;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        body.velocity = new Vector2(horizontal * dashIntensity, body.velocity.y);

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;

        yield return null;
    }

    private IEnumerator Death()
    {
        body.velocity = Vector2.zero;
        yield return new WaitForSeconds(deathDelay);
        isDead = false;
        transform.position = spawnPoint.position;
    }
}
