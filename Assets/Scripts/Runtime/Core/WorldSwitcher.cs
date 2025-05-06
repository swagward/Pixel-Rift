using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public static bool InNormalWorld { get; set; } = true;
    [SerializeField] private GameObject normalWorld;
    [SerializeField] private GameObject alteredWorld;
    [SerializeField] private KeyCode shiftKey = KeyCode.F;
    
    private void Update()
    {
        if (!Input.GetKeyDown(shiftKey)) return;
        
        InNormalWorld = !InNormalWorld;
        Switch();
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
    }
}
