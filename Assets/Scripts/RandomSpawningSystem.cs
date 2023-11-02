using System.Collections;
using UnityEngine;

public class RandomSpawningSystem : SpawnManager
{
    [SerializeField] private Vector2 initialPosition;
    [SerializeField] private float distance;
    [SerializeField] private float minimumHeight;
    [SerializeField] private float maximumHeight;
    [SerializeField] private float spawnDelay;
    private static Vector2 lastPosition;
    private GameObject lastSpawned;

    protected override void Awake()
    {
        base.Awake();
        lastPosition = initialPosition;
    }

    private void Start()
    {
        StartCoroutine(SpawnRandomPlatforms());
    }

    private IEnumerator SpawnRandomPlatforms()
    {
        while(true)
        {
            lastSpawned = PlatformGenerator();
            yield return new WaitForSeconds(spawnDelay);
            lastPosition = lastSpawned != null ? lastSpawned.transform.position : lastPosition;
        }
    }

    private GameObject PlatformGenerator()
    {
        GameObject tempSpawn = GetClone();

        if (tempSpawn == null) return null;

        tempSpawn.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        tempSpawn.transform.localEulerAngles = Vector3.zero;
        tempSpawn.transform.position = randomPosition;

        tempSpawn.SetActive(true);

        return tempSpawn;
    }

    private Vector2 randomPosition
    {
        get
        {
            float height = Random.Range(minimumHeight, maximumHeight);

            Vector2 newPosition = lastPosition;

            newPosition.y = height;
            newPosition.x += distance;

            return newPosition;
        }
    }
}
