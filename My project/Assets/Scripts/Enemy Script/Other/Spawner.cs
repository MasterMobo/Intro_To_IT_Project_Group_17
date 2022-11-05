using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject swarmPrefabs;

    [SerializeField]
    float spawnInterval;

    [SerializeField]
    int swarmSize = 100;

    int currentSwarmCount = 0;

    private void Start()
    {
        StartCoroutine(spawnEnemy(spawnInterval, swarmPrefabs));
    }

    // Co-routine to spawn enemy endlessly with delay
    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        // Create new enemy object with Instantiate() (Don't worry about the Quaternion.identity, just remember to add it)
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f)), Quaternion.identity);
        currentSwarmCount++;
        if (currentSwarmCount < swarmSize)
        {
            StartCoroutine(spawnEnemy(spawnInterval, swarmPrefabs));
        }
    }
}
