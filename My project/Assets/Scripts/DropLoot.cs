using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLoot : MonoBehaviour
{
    public List<GameObject> lootPool = new List<GameObject>(); // List of loot that can be dropped
    int dropN; // How many items will be dropped

    // Start is called before the first frame update
    void Start()
    {
        dropN = Random.Range(0, lootPool.Count + 1);
    }

    public void SpawnLoot()
    {
        for (int i = 0; i < dropN; i++)
        {
            Instantiate(lootPool[Random.Range(0, lootPool.Count)], transform.position, Quaternion.identity);
        }
    }
}
