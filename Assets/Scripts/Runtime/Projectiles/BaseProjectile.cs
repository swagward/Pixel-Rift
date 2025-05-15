using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    
    public float speed;
    public int damage;
    public float lifeTime;
    
    protected virtual void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    public virtual void Attack(Vector3 direction)
    {
        rb.AddForce(direction.normalized * speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    [System.Serializable]
    public class MagicAttack
    {
        public string name;
        public GameObject prefab;
        public int manaCost;
    }
}
