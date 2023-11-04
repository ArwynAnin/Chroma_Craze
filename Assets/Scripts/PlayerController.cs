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

    public static Renderer colorIndicator;
    private bool isDead;

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

            return grounded;
        }
    }

    #endregion

    private void Awake()
    {
        isDead = false;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();

        colorIndicator = ui.GetComponent<Renderer>();

        deathBlocks = new string[] { "Border", "Saw", "Spikes", "MainCamera"};
    }

    private void Update()
    {
        if (isDead) return;
        Run();
        Jump();
        ColorChanger();
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
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void ColorChanger()
    {
        colorIndicator.material = Input.GetKeyDown(KeyCode.Alpha1) ? colors[0] :
            Input.GetKeyDown(KeyCode.Alpha2) ? colors[1] :
            Input.GetKeyDown(KeyCode.Alpha3) ? colors[2] :
            colorIndicator.material;
    }

    private IEnumerator Death()
    {
        body.velocity = Vector2.zero;
        yield return new WaitForSeconds(deathDelay);

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
