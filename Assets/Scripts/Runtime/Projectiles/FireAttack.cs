using UnityEngine;

public class FireAttack : BaseProjectile
{
    protected override void Start()
    {
        base.Start();
    }
    
    public override void Attack(Vector3 direction)
    {
        base.Attack(direction);
        Debug.Log("fireball");
    }
}
