using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Chaser : MonoBehaviour
{
    [SerializeField] private GameObject flag;
    [SerializeField] protected Animator sprite;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed;

    private void Update()
    {
        if (flag.gameObject.activeSelf) return;
        StartCoroutine(pointToPointTravel());
    }
    private IEnumerator pointToPointTravel()
    {
        while (sprite.transform.position != pointB.position)
        {
            Vector3 origin = sprite.transform.position;
            sprite.transform.position = Vector3.MoveTowards(origin, pointB.position, speed * Time.deltaTime);
            yield return null;
        }

        yield return null;
    }
}
