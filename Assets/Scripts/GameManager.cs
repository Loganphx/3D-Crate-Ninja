using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{

    [SerializeField] 
    private List<Target> targetPrefabs;
    
    [SerializeField] 
    private List<Target> spawnedTargets;

    [SerializeField] private float spawnRate = 1.0f;

    private void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    private void FixedUpdate()
    {
        foreach (var target in spawnedTargets)
        {
            if (target.transform.position.y < -7)
            {
                target.OnDeath -= OnTargetDestroyed;
                Destroy(target.gameObject);
                spawnedTargets.Remove(target);
            }
        }
    }


    private IEnumerator SpawnTargets()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targetPrefabs.Count);
            var target = Instantiate<Target>(targetPrefabs[index]);
            target.OnDeath += OnTargetDestroyed;
            spawnedTargets.Add(target);
        }
    }
    
    private void OnTargetDestroyed(Target target)
    {
        target.OnDeath -= OnTargetDestroyed;
        Destroy(target.gameObject);
        spawnedTargets.Remove(target);
    }
}
