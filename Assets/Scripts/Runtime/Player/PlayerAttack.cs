using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject projectilePrefab;
    
    private const float MaxMana = 100;
    [SerializeField] private float currentMana;
    [SerializeField] private Slider manaSlider;

    [SerializeField] private BaseProjectile.MagicAttack[] attacks;
    [SerializeField] private int currentAttackIndex = 0;

    private void Start()
    {
        currentMana = MaxMana;
        manaSlider.value = currentMana;
        StartCoroutine(ManaRegen());
    }
    
    private void Update()
    {
        if (PlayerMovement.IsDead) return;

        var selectedAttack = attacks[currentAttackIndex];
        if (Input.GetKeyDown(KeyCode.Mouse0) && currentMana >= selectedAttack.manaCost)
        {
            var projectileObj = Instantiate(selectedAttack.prefab, firePos.position, firePos.rotation);
            
            //slight deviation to show its a low level attack.
            var direction = firePos.forward;
            
            var deviationAngle = 5f; // degrees
            direction = Quaternion.Euler(
                Random.Range(-deviationAngle, deviationAngle),
                Random.Range(-deviationAngle, deviationAngle),
                0f) * direction;

            var projectile = projectileObj.GetComponent<BaseProjectile>();
            projectile.Attack(direction);

            currentMana -= selectedAttack.manaCost;
            manaSlider.value = currentMana;
        }
    }
    
    private IEnumerator ManaRegen()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            if (currentMana < MaxMana)
            {
                currentMana = Mathf.Min(currentMana + 20, MaxMana);
                manaSlider.value = currentMana;
            }
        }
    }
}
