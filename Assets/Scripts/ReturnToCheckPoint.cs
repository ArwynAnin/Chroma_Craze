using UnityEngine;
using UnityEngine.Tilemaps;

public class ReturnToCheckPoint : MonoBehaviour
{
    private TilemapRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<TilemapRenderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) Debug.Log("hit");
        if (_renderer.material.color == PlayerController.colorIndicator.material.color || !other.gameObject.CompareTag("Player")) return;
        other.gameObject.transform.position = PlayerController.spawnPoint.position;
        Debug.Log("reset");
    }
}
