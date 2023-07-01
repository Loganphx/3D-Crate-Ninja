using UnityEngine;

public static class TargetExtensions
{
    public static Vector3 RandomForce(this Target target)
    {
        return Vector3.up * Random.Range(target.MinSpeed, target.MaxSpeed);
    }

    public static float RandomTorque(this Target target)
    {
        return Random.Range(-target.MaxTorque, target.MaxTorque);
    }

    public static Vector3 RandomSpawnPos(this Target target)
    {
        return new Vector3(Random.Range(-target.XRange, target.XRange), target.YSpawnPos);
    }
}