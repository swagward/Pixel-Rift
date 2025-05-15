using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    private static bool InNormalWorld { get; set; } = true;
    [SerializeField] private GameObject normalWorld;
    [SerializeField] private GameObject alteredWorld;
    [SerializeField] private KeyCode shiftKey = KeyCode.F;
    private PlayerAttack _playerMana;

    private void Start()
    {
        _playerMana = GameObject.Find("PlayerRoot").GetComponent<PlayerAttack>();
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(shiftKey) && _playerMana.currentMana >= 25)
        {
            InNormalWorld = !InNormalWorld;
            Switch();
        }
    }

    private void Switch()
    {
        switch (InNormalWorld)
        {
            case true:
                normalWorld.SetActive(true);
                alteredWorld.SetActive(false);
                break;
            case false:
                normalWorld.SetActive(false);
                alteredWorld.SetActive(true);
                break;
            
        }
        
        _playerMana.currentMana -= 25;
        _playerMana.manaSlider.value = _playerMana.currentMana;
    }
}
