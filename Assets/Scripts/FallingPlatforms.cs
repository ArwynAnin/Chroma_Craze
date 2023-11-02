using UnityEngine;
using UnityEngine.Tilemaps;

public class FallingPlatforms : MovingPlatforms
{
    private TilemapRenderer _renderer;

    protected override void Awake()
    {
        _renderer = GetComponentInChildren<TilemapRenderer>();
        base.Awake();
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        // if (_renderer.material.color == PlayerController.colorIndicator.material.color) score++;
        if (_renderer.material.color == PlayerController.colorIndicator.material.color) return;
        isFalling = true;
        body.bodyType = RigidbodyType2D.Dynamic;
    }
}
