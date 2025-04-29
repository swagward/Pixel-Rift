using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
    public GameObject world;
    public GameObject otherWorld;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (world.activeSelf)
            {
                world.SetActive(false);
                otherWorld.SetActive(true);
            }
            else
            {
                world.SetActive(true);
                otherWorld.SetActive(false);
            }
        }
    }
}
