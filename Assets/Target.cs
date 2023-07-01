using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -6;
    
    void Awake()
    {
        targetRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return UnityEngine.Random.Range(minSpeed, maxSpeed);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(UnityEngine.Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
