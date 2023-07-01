using UnityEngine;

public abstract class Target : MonoBehaviour
{
    public delegate void TargetDeath(Target target);

    private Rigidbody _targetRb;

    [SerializeField] protected float minSpeed = 12;
    [SerializeField] protected float maxSpeed = 16;
    [SerializeField] protected float maxTorque = 10;
    [SerializeField] protected float xRange = 4;
    [SerializeField] protected float ySpawnPos = -6;

    public TargetDeath OnDeath { get; set; }
    
    private void Awake()
    {
        _targetRb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _targetRb.AddForce(this.RandomForce(), ForceMode.Impulse);
        _targetRb.AddTorque(this.RandomTorque(), this.RandomTorque(), this.RandomTorque(), ForceMode.Impulse);
        transform.position = this.RandomSpawnPos();
    }

    private void OnMouseDown()
    {
        OnDeath?.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        OnDeath?.Invoke(this);
    }
    
    public Rigidbody TargetRigidbody => _targetRb;

    public float MinSpeed => minSpeed;
    public float MaxSpeed => maxSpeed;
    public float MaxTorque => maxTorque;
    public float XRange => xRange;
    public float YSpawnPos => ySpawnPos;
}
