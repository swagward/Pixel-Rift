using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement _player;
    private const int MaxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //sfx too
        
        if(currentHealth <= 0)
            _player.Die();
    }
}
