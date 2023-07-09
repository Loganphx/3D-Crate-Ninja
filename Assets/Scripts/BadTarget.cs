public class BadTarget : Target
{
    protected override void Awake()
    {
        base.Awake();
        value = -1;
    }
}