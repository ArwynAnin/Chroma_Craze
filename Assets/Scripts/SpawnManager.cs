using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] targetObject;
    [SerializeField] private int spawnCount;

    private GameObject[] spawnedObjects;
    private void Awake()
    {
        spawnedObjects = new GameObject[spawnCount];
        for (int current = 0; current < spawnedObjects.Length; current++)
        {
            int randomizedInt = Random.Range(0, targetObject.Length);
            spawnedObjects[current] = Instantiate(targetObject[randomizedInt], transform);
            spawnedObjects[current].SetActive(false);
        }
    }

    protected GameObject GetClone()
    {
        for (int current = 0; current < spawnedObjects.Length;current++)
        {
            if (!spawnedObjects[current].activeSelf && spawnedObjects[current] != null) return spawnedObjects[current];
        }
        return null;
    }
}
