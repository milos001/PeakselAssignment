using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorSpawn : MonoBehaviour
{
    private int counter;
    private int[] usedSpawns;
    
    private float spawnRate;
    private float spawnIncreaseAmount = .9f;
    private float spawnIncreaseInterval = 15f;
    private float speedUpAmount = 1.1f;
    private float speedUpInterval = 10f;

    public Transform[] spawnPositions;

    public GameObject[] items;
    
    // Start is called before the first frame update
    void Start()
    {
        spawnRate = 2f;
        usedSpawns = new[] {-1, -1, -1, -1, -1};
        
        Invoke(nameof(SpawnItem), 1f);
        InvokeRepeating(nameof(SpeedUp), speedUpInterval, speedUpInterval);
        InvokeRepeating(nameof(IncreaseSpawns), spawnIncreaseInterval, spawnIncreaseInterval);
    }

    private void SpeedUp()
    {
        foreach (var t in items)
        {
            t.GetComponent<ItemFall>().fallRate *= speedUpAmount;
        }
    }

    private void IncreaseSpawns()
    {
        spawnRate *= spawnIncreaseAmount;
    }
    
    private void SpawnItem()
    {
        beginning:
        int randomItem = Random.Range(0, spawnPositions.Length);
        int randomPos = Random.Range(0, spawnPositions.Length);

        foreach (var t in usedSpawns)
        {
            if(randomPos == t) goto beginning;
        }

        Instantiate(items[randomItem], spawnPositions[randomPos].position, Quaternion.identity);
        usedSpawns[counter] = randomPos;
        counter++;
        if (counter == spawnPositions.Length)
        {
            usedSpawns = new[] {-1, -1, -1, -1, -1};
            counter = 0;
        }
        
        Invoke(nameof(SpawnItem), spawnRate);
    }
}