using System.Collections;
using UnityEngine;

public class PtpPlatforms : MonoBehaviour
{
    [SerializeField] protected Animator sprite;
    [SerializeField] private Transform[] points;
    [SerializeField] private float speed;
    [SerializeField] private float stopDelay;


    void Start()
    {
        StartCoroutine(pointToPointTravel());
    }

    private IEnumerator pointToPointTravel()
    {
        while (true)
        {
            Vector3 pointA = points[0].position;
            Vector3 pointB = points[1].position;

            while (sprite.transform.position != pointB)
            {
                Vector3 origin = sprite.transform.position;
                sprite.transform.position = Vector3.MoveTowards(origin, pointB, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(stopDelay);

            while (sprite.transform.position != pointA)
            {
                Vector3 origin = sprite.transform.position;
                sprite.transform.position = Vector3.MoveTowards(origin, pointA, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(stopDelay);

            yield return null;
        }
    }
}
