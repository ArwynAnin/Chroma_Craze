using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private float speed;
    protected Rigidbody2D body;
    protected bool isFalling;

    protected virtual void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        isFalling = false;
    }

    private void Update()
    {
        if (isFalling) return;
        body.velocity = Vector2.left * speed;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!(other.CompareTag("MainCamera") || other.CompareTag("Border"))) return;
        isFalling = false;
        gameObject.SetActive(false);
    }
}
