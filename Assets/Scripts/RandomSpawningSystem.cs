using System.Collections;
using UnityEngine;

public class RandomSpawningSystem : SpawnManager
{
    [SerializeField] private Vector2 initialPosition;
    [SerializeField] private float minimumDistance;
    [SerializeField] private float maximumDistance;
    [SerializeField] private float minimumHeight;
    [SerializeField] private float maximumHeight;
    [SerializeField] private float spawnDelay;
    private static Vector2 lastPosition;

    private Vector2 randomPosition
    {
        get 
        {
            Vector2 newPosition = lastPosition == Vector2.zero ? initialPosition : lastPosition;
            float distance = Random.Range(minimumDistance, maximumDistance);
            float height = Random.Range(minimumHeight, maximumHeight);
            newPosition.x += distance;
            newPosition.y = height;

            return newPosition;
        }
    }

    private void Start()
    {
        lastPosition = Vector2.zero;
        StartCoroutine(SpawnRandomPlatforms());
    }

    private IEnumerator SpawnRandomPlatforms()
    {
        while(true)
        {
            PlatformGenerator();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void PlatformGenerator()
    {
        GameObject tempSpawn = GetClone();
        if (tempSpawn == null) return;
        tempSpawn.SetActive(true);

        tempSpawn.transform.position = randomPosition;
        lastPosition = randomPosition;
    }
}
