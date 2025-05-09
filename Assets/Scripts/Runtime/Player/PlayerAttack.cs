using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject projectilePrefab;
    
    private void Update()
    {
        if (PlayerMovement.IsDead) return;
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var projectile  = Instantiate(projectilePrefab, firePos.transform.position, firePos.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(firePos.forward * 15, ForceMode.Impulse);
        }
    }
}
