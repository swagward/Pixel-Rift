using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    protected virtual void Start()
    {
        Destroy(this.gameObject, 2.5f);
    }
}
