using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private PlayerMovement _player;
    private const int MaxHealth = 100;
    [SerializeField] private int currentHealth;
    [SerializeField] private Slider healthSlider;

    private void Start()
    {
        _player = GetComponent<PlayerMovement>();
        currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthSlider.value = currentHealth;
        //sfx too
        
        if(currentHealth <= 0)
            _player.Die();
    }
}
