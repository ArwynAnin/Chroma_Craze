using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.velocity = Vector2.left * speed;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("MainCamera")) return;
        gameObject.SetActive(false);
    }
}
