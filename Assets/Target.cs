using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody _targetRb;

    [SerializeField] private float minSpeed = 12;
    [SerializeField] private float maxSpeed = 16;
    [SerializeField] private float maxTorque = 10;
    [SerializeField] private float xRange = 4;
    [SerializeField] private float ySpawnPos = -6;
    
    public delegate void TargetDeath(Target target);
    public event TargetDeath OnDeath;
    private void Awake()
    {
        _targetRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        OnDeath?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnDeath?.Invoke(this);
    }
}
