using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Transform firePos;
    public GameObject projectilePrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject projectile;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            projectile  = Instantiate(projectilePrefab,firePos.transform.position, firePos.rotation);
            
            projectile.GetComponent<Rigidbody>().AddForce(firePos.forward * 15, ForceMode.Impulse);
        }
    }
}
