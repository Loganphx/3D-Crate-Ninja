using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GameManager : MonoBehaviour
{

    public List<Target> targetPrefabs;
    public List<Target> spawnedTargets;

    private float spawnRate = 1.0f;

    private void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    private IEnumerator SpawnTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = UnityEngine.Random.Range(0, targetPrefabs.Count);
            spawnedTargets.Add(Instantiate(targetPrefabs[index]));
        }
    }
}
