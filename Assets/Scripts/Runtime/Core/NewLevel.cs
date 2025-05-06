using UnityEngine;

public class NewLevel : MonoBehaviour
{
    private PlayerMovement player;
    
    [Header("Checkpoint")] [SerializeField]
    private Transform nextPlayerCheckpoint;
    
    [Header("Level Narration")]
    [SerializeField] private AudioSource playerSrc;
    [SerializeField] private AudioClip audioClip;
    
    private void Start()
    {
        player = GameObject.Find("PlayerRoot").GetComponent<PlayerMovement>();
        playerSrc = player.GetComponent<AudioSource>();
    }
        
    private void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Player")) return;
        playerSrc.clip = audioClip;
        playerSrc.Play();

        player.lastCheckPoint = nextPlayerCheckpoint;
        GetComponent<BoxCollider>().enabled = false; //stops player from colliding with trigger again x
    }
}
