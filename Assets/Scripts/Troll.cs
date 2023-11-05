using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : MonoBehaviour
{
    [SerializeField] private float delay;
    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(Drop());
    }

    private IEnumerator Drop()
    {
        yield return new WaitForSeconds(delay);
        body.bodyType = RigidbodyType2D.Dynamic;
        transform.parent = null;
        yield return null;
    }
}
