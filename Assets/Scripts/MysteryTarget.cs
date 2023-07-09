using Unity.VisualScripting;
using UnityEngine;

public class MysteryTarget : GoodTarget
{
    protected override void Awake()
    {
        base.Awake();
        value = Random.Range(1,5);
    }
}